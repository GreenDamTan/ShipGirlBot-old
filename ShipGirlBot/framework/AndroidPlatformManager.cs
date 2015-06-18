using System;


public class AndroidPlatformManager : MonoBehaviour
{
    private static AndroidPlatformManager mInstance;
    public string usingChannel = "1";
    public PlatformType usingPlatform = PlatformType.Self;


    public string ChannelId
    {
        get
        {
            return usingChannel;
        }
    }

    public static AndroidPlatformManager instance
    {
        get
        {
            if (mInstance == null)
            {
               // mInstance = UnityEngine.Object.FindObjectOfType(typeof(AndroidPlatformManager)) as AndroidPlatformManager;
                mInstance = new AndroidPlatformManager();
            }
            return mInstance;
        }
    }

    public bool IsIosPlatform
    {
        get
        {
            return (this.usingPlatform == PlatformType.Ios);
        }
    }

    public bool IsIosAppStore
    {
        get
        {
            return ((this.usingPlatform == PlatformType.Ios) && (this.ChannelId == "2"));
        }
    }
    public bool IsLenjingPlatform
    {
        get
        {
            return (this.usingPlatform == PlatformType.Lengjing);
        }
    }

    public bool IsSelfPlatform
    {
        get
        {
            return (((this.usingPlatform == PlatformType.Self) || (this.usingPlatform == PlatformType.Zero)) || (this.usingPlatform == PlatformType.Ios));
        }
    }

    public bool IsUCPlatform
    {
        get
        {
            return (this.usingPlatform == PlatformType.UC);
        }
    }

    public bool NeedHideChangeUserBtn
    {
        get
        {
            return (this.usingPlatform == PlatformType.UC);
        }
    }

    public bool NeedSelfHanldeUserLogin
    {
        get
        {
            return (((this.usingPlatform == PlatformType.Self) || (this.usingPlatform == PlatformType.Zero)) || (this.usingPlatform == PlatformType.Ios));
        }
    }
}

