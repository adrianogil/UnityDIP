using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RGBChannels : MonoBehaviour
{
    [HideInInspector] public bool redChannel = true;
    [HideInInspector] public bool greenChannel = true;
    [HideInInspector] public bool blueChannel = true;

    public void ApplyFilter()
    {
        float[] channelMask = new float[4];
        channelMask[0] = redChannel? 1f : 0f;
        channelMask[1] = greenChannel? 1f : 0f;
        channelMask[2] = blueChannel? 1f : 0f;
        channelMask[3] = 1f;

        Shader.SetGlobalFloatArray("_RGBMask", channelMask);
        Shader.SetGlobalFloat("_RGBMaskLength", channelMask.Length);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(RGBChannels))]
public class RGBChannelsEditor : Editor {

    public void OnEnable()
    {
        RGBChannels channels = target as RGBChannels;

        if (channels == null) return;
        
        channels.ApplyFilter();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RGBChannels channels = target as RGBChannels;

        if (channels == null) return;

        bool changes = false;

        bool lastValue = channels.redChannel;
        channels.redChannel = EditorGUILayout.Toggle("Red Channel", lastValue);
        if (lastValue != channels.redChannel) changes = true;

        lastValue = channels.greenChannel;
        channels.greenChannel = EditorGUILayout.Toggle("Green Channel", lastValue);
        if (lastValue != channels.greenChannel) changes = true;

        lastValue = channels.blueChannel;
        channels.blueChannel = EditorGUILayout.Toggle("Blue Channel", lastValue);
        if (lastValue != channels.blueChannel) changes = true;

        if (changes)
        {
            channels.ApplyFilter();
        }
    }

}
#endif
