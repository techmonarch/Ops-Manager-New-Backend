using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;
public enum FileType
{
    [Description(".jpg,.png,.jpeg")]
    Image,
    [Description(".jpg,.png,.jpeg")]
    Video
}