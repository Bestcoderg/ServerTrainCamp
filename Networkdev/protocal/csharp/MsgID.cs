// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MsgID.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from MsgID.proto</summary>
public static partial class MsgIDReflection {

  #region Descriptor
  /// <summary>File descriptor for MsgID.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static MsgIDReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgtNc2dJRC5wcm90byp4CgVNU0dJRBINCglNU0dfQkVHSU4QABIQCgtNU0df",
          "VEVTVF9JRBDtBxIXChJNU0dfUExBWUVSX01PVkVfSUQQ7gcSGQoUTVNHX1BM",
          "QVlFUl9BVFRBQ0tfSUQQ7wcSGgoVTVNHX1BMQVlFUl9SRUdJU1RFX0lEEPAH",
          "YgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::MSGID), }, null));
  }
  #endregion

}
#region Enums
public enum MSGID {
  [pbr::OriginalName("MSG_BEGIN")] MsgBegin = 0,
  [pbr::OriginalName("MSG_TEST_ID")] MsgTestId = 1005,
  [pbr::OriginalName("MSG_PLAYER_MOVE_ID")] MsgPlayerMoveId = 1006,
  [pbr::OriginalName("MSG_PLAYER_ATTACK_ID")] MsgPlayerAttackId = 1007,
  [pbr::OriginalName("MSG_PLAYER_REGISTE_ID")] MsgPlayerRegisteId = 1008,
}

#endregion


#endregion Designer generated code
