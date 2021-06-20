# WIP - playing around, making F# interop with Win32+DirectX

My thoughts:
- GUIDs are important!
- Order of methods in COM is important!
- Number of methods is important!
- Debug into method to check which one is called
- When you need return value (it returns something sensible, e.g. a buffer  location or size) - use [\<PreserveSig\>]
- Debug layer is nice
- Dont trust MSDN - check headers
