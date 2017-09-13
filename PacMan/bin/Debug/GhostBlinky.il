
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.17929




// Metadata version: v4.0.30319
.assembly extern PacMan
{
  .publickeytoken = (CD 85 BB 7E F1 3E CE 43 )                         // ...~.>.C
  .ver 1:0:0:0
}
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly GhostBlinky
{
  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 1A 2E 4E 45 54 46 72 61 6D 65 77 6F 72 6B   // ....NETFramework
                                                                                                        2C 56 65 72 73 69 6F 6E 3D 76 34 2E 35 01 00 54   // ,Version=v4.5..T
                                                                                                        0E 14 46 72 61 6D 65 77 6F 72 6B 44 69 73 70 6C   // ..FrameworkDispl
                                                                                                        61 79 4E 61 6D 65 12 2E 4E 45 54 20 46 72 61 6D   // ayName..NET Fram
                                                                                                        65 77 6F 72 6B 20 34 2E 35 )                      // ework 4.5
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 

  // --- Следующий настраиваемый атрибут добавлен автоматически, не следует отменять преобразование в комментарий -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.
  .publickey = (00 24 00 00 04 80 00 00 94 00 00 00 06 02 00 00   // .$..............
                00 24 00 00 52 53 41 31 00 04 00 00 01 00 01 00   // .$..RSA1........
                BD D1 DF 8A C1 C0 72 42 06 10 C8 CB 18 03 13 23   // ......rB.......#
                FB 76 70 20 F9 F2 F8 FA 69 71 55 34 51 5A DA 4A   // .vp ....iqU4QZ.J
                94 39 40 7D 14 34 70 A5 55 53 71 34 6C F4 F7 CC   // .9@}.4p.USq4l...
                82 82 A7 19 62 E6 CE A5 19 4E 3A DB A1 CB 51 01   // ....b....N:...Q.
                93 02 71 F0 B1 B9 87 2A 23 8F 7E 77 7A AF F6 5F   // ..q....*#.~wz.._
                89 94 21 E7 CE A4 10 3B 07 53 A3 A8 BE 7F 78 7E   // ..!....;.S....x~
                41 AB BE AB 1E 0F 75 6B D2 2C 8D 64 F0 C5 58 74   // A.....uk.,.d..Xt
                6C C7 EE 59 D2 5A BF 07 FC 28 6B 4A 06 CC F8 E9 ) // l..Y.Z...(kJ....
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module GhostBlinky.dll
// MVID: {23D0813C-45CD-4ED5-8F5F-602EA8C566F4}
.imagebase 0x10000000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000009    //  ILONLY
// Image base: 0x00430000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi sealed beforefieldinit GhostBlinky.GhostBlinky
       extends [PacMan]PacMan.model.Ghost
{
  .field private class GhostBlinky.Stub _stub
  .method public hidebysig specialname rtspecialname 
          instance void  .ctor(int32 y,
                               int32 x,
                               class [PacMan]PacMan.model.Cell[][] 'field') cil managed
  {
    // Размер кода:       13 (0xd)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  ldarg.2
    IL_0003:  ldarg.3
    IL_0004:  call       instance void [PacMan]PacMan.model.Ghost::.ctor(int32,
                                                                         int32,
                                                                         class [PacMan]PacMan.model.Cell[][])
    IL_0009:  nop
    IL_000a:  nop
    IL_000b:  nop
    IL_000c:  ret
  } // end of method GhostBlinky::.ctor

  .method public hidebysig virtual instance class [PacMan]PacMan.model.Cell 
          Move() cil managed
  {
    // Размер кода:       52 (0x34)
    .maxstack  5
    .locals init ([0] int32 wayLength,
             [1] class [PacMan]PacMan.model.Cell CS$1$0000,
             [2] bool CS$4$0001)
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldarg.0
    IL_0003:  call       instance class [PacMan]PacMan.model.PacMan [PacMan]PacMan.model.Ghost::get_PacMan()
    IL_0008:  callvirt   instance int32 [PacMan]PacMan.model.Monster::get_Y()
    IL_000d:  ldarg.0
    IL_000e:  call       instance class [PacMan]PacMan.model.PacMan [PacMan]PacMan.model.Ghost::get_PacMan()
    IL_0013:  callvirt   instance int32 [PacMan]PacMan.model.Monster::get_X()
    IL_0018:  ldloca.s   wayLength
    IL_001a:  ldc.i4.0
    IL_001b:  call       instance bool [PacMan]PacMan.model.Ghost::SetShortestWayDirection(int32,
                                                                                           int32,
                                                                                           int32&,
                                                                                           bool)
    IL_0020:  stloc.2
    IL_0021:  ldloc.2
    IL_0022:  brtrue.s   IL_0029

    IL_0024:  nop
    IL_0025:  ldnull
    IL_0026:  stloc.1
    IL_0027:  br.s       IL_0032

    IL_0029:  ldarg.0
    IL_002a:  call       instance class [PacMan]PacMan.model.Cell [PacMan]PacMan.model.Ghost::MakeGhostMove()
    IL_002f:  stloc.1
    IL_0030:  br.s       IL_0032

    IL_0032:  ldloc.1
    IL_0033:  ret
  } // end of method GhostBlinky::Move

  .method assembly hidebysig instance void 
          Stub() cil managed
  {
    // Размер кода:       25 (0x19)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  newobj     instance void GhostBlinky.Stub::.ctor()
    IL_0007:  stfld      class GhostBlinky.Stub GhostBlinky.GhostBlinky::_stub
    IL_000c:  ldarg.0
    IL_000d:  ldfld      class GhostBlinky.Stub GhostBlinky.GhostBlinky::_stub
    IL_0012:  callvirt   instance void GhostBlinky.Stub::StubMethod()
    IL_0017:  nop
    IL_0018:  ret
  } // end of method GhostBlinky::Stub

} // end of class GhostBlinky.GhostBlinky

.class private auto ansi sealed beforefieldinit GhostBlinky.Stub
       extends [mscorlib]System.Object
{
  .field private class GhostBlinky.GhostBlinky _ghost
  .method assembly hidebysig instance void 
          StubMethod() cil managed
  {
    // Размер кода:       28 (0x1c)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  ldc.i4.0
    IL_0003:  ldc.i4.0
    IL_0004:  ldnull
    IL_0005:  newobj     instance void GhostBlinky.GhostBlinky::.ctor(int32,
                                                                      int32,
                                                                      class [PacMan]PacMan.model.Cell[][])
    IL_000a:  stfld      class GhostBlinky.GhostBlinky GhostBlinky.Stub::_ghost
    IL_000f:  ldarg.0
    IL_0010:  ldfld      class GhostBlinky.GhostBlinky GhostBlinky.Stub::_ghost
    IL_0015:  callvirt   instance void GhostBlinky.GhostBlinky::Stub()
    IL_001a:  nop
    IL_001b:  ret
  } // end of method Stub::StubMethod

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Размер кода:       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Stub::.ctor

} // end of class GhostBlinky.Stub


// =============================================================

// *********** ДИЗАССЕМБЛИРОВАНИЕ ЗАВЕРШЕНО ***********************
// ВНИМАНИЕ: создан файл ресурсов Win32 D:\mercurial\PacMan\PacMan\bin\Debug\GhostBlinky.res
