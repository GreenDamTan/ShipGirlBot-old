using System;
////using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public bool disableBrokenImage = false;
    private static GameInfo mInstance;
    private string mUA = "";
    private string mUv = "4.6.3p3";


    public string UV
    {
        get
        {
            if (AndroidPlatformManager.instance.usingPlatform == PlatformType.Self)
            {
                return "4.6.4f1";
            }
            if (AndroidPlatformManager.instance.usingPlatform == PlatformType.Ios)
            {
                if (AndroidPlatformManager.instance.usingChannel.Equals("1"))
                {
                    return "4.6.4f1";
                }
                else if (AndroidPlatformManager.instance.usingChannel.Equals("2"))
                {
                    return "4.6.4f1";
                }
            }
            return "4.6.4f1";
        }
        set
        {
            mUv = value;
        }
    }

    public string version
    {
        get
        {
            if( AndroidPlatformManager.instance.usingPlatform == PlatformType.Self)
            {
                return (string)tools.configmng.instance.getval("version7");
            }
            if (AndroidPlatformManager.instance.usingPlatform == PlatformType.Ios)
            {
                if (AndroidPlatformManager.instance.usingChannel.Equals("1"))
                {
                    return (string)tools.configmng.instance.getval("version7");
                }else if(AndroidPlatformManager.instance.usingChannel.Equals("2"))
                {
                    return (string)tools.configmng.instance.getval("version7");
                }
            }

            return (string)tools.configmng.instance.getval("version7");
        }
    }

    public string UA
    {
        get
        {
            return mUA;
        }
        set
        {
            mUA = value;
        }
    }

    public static GameInfo instance
    {
        get
        {
            //mInstance = UnityEngine.Object.FindObjectOfType(typeof(GameInfo)) as GameInfo;
            if (mInstance == null)
            {
                mInstance = new GameInfo();
                //mInstance = new GameObject { name = typeof(GameInfo).ToString() }.AddComponent<GameInfo>();
            }
            return mInstance;
        }
    }
}

