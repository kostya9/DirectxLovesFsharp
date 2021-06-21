# WIP - playing around, making F# interop with Win32+DirectX

My thoughts:
- GUIDs are important!
- Order of methods in COM is important!
- Number of methods is important!
- Debug into method to check which one is called
- When you need return value (it returns something sensible, e.g. a buffer  location or size) - use [\<PreserveSig\>]
- Debug layer is nice
- Dont trust MSDN - check headers
- sizeof<> - nice operator to debug struct sizes (if you messed up something)
- fsharp float=Double, csharp float=Single
- You cannot just take a pointer to an array - you need to make sure it is marshalled in correct way