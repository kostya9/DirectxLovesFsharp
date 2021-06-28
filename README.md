# WIP - playing around, making F# interop with Win32+DirectX

My thoughts:
- GUIDs are important!
- Order of methods in COM is important!
- Number of methods is important!
- Debug into method to check which one is called
- When you need return value (it returns something sensible, e.g. a buffer  location or size) - use [\<PreserveSig\>]
```fsharp
[<PreserveSig>]
abstract member GetGPUVirtualAddress:
    unit -> uint64
```
- DirectX Debug layer is nice
- Dont trust MSDN - check headers
- sizeof<> - nice operator to debug struct sizes (if you messed up something)
- fsharp float=Double, csharp float=Single
- You cannot just take a pointer to an array - you need to make sure it is marshalled in correct way
- Unchecked.defaultof<> is a nice way to initialize variables that will later be filled by COM
```fsharp
let mutable pVertexDataBegin = Unchecked.defaultof<voidptr>
vertexBuffer.Map(0u, &readRange, &pVertexDataBegin)
```
- Copying native data is easy if you convert the location to span beforehand:
```fsharp
let verticesDataSpan = new Span<Vertex>(pVertexDataBegin, vertices.Length)
vertices.CopyTo(verticesDataSpan)
```
- Make sure you release COM objects or tie them to GC Roots so that strange finalizer boogey-woogey internal CLR errors won't occur (alternatively, create nice wrappers that implement finalizers/disposables. oh well, I am too lazy for that)