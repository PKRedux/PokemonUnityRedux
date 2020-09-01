using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
[EditorTool("Tile Editor")]
class TileEditor : EditorTool
{
    [SerializeField]
    Texture2D m_ToolIcon;
    GUIContent m_IconContent;
    void OnEnable()
    {
        m_IconContent = new GUIContent()
        {
            image = m_ToolIcon,
            text = "Tile Editor",
            tooltip = "Use this tool for map editing."
        };
    }

    public override GUIContent toolbarIcon
    {
        get { return m_IconContent; }
    }
    public override void OnToolGUI(EditorWindow window)
    {
        EditorGUI.BeginChangeCheck();
        Vector3 position = Tools.handlePosition;
        if(Event.current.type == EventType.KeyDown) {
            switch(Event.current.keyCode)
            {
                case KeyCode.KeypadDivide: //rotate
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.Rotate(0,90,0,Space.World);
                    break;
                case KeyCode.Space: //rotate
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.Rotate(0,90,0,Space.World);
                    break;
                case KeyCode.Equals: //up
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,1,0);
                    break;
                case KeyCode.Minus: //down
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,-1,0);
                    break;
                case KeyCode.KeypadPlus: //up
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,1,0);
                    break;
                case KeyCode.KeypadMinus: //down
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,-1,0);
                    break;
                case KeyCode.Keypad8: //forward
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,0,1);
                    break;
                case KeyCode.Keypad6: //left
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(1,0,0);
                    break;
                case KeyCode.Keypad2: //back
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(0,0,-1);
                    break;
                case KeyCode.Keypad4: //right
                    Event.current.Use();
                    foreach (var transform in Selection.transforms)
                        transform.position += new Vector3(-1,0,0);
                    break;
                case KeyCode.KeypadMultiply: //top view
                    GameObject tempObj = new GameObject("CameraPos");
                    tempObj.transform.position = new Vector3(0,15,0);
                    tempObj.transform.rotation = Quaternion.Euler(90,0,0);
                    SceneView.lastActiveSceneView.AlignViewToObject(tempObj.transform);
                    DestroyImmediate(tempObj);
                    break;
                case KeyCode.Keypad5: //player view
                    SceneView.lastActiveSceneView.orthographic = !SceneView.lastActiveSceneView.orthographic;
                    MapEditorCamera.CopyCamera();
                    break;
                case KeyCode.Keypad0: //reset view
                    SceneView.lastActiveSceneView.orthographic = false;
                    MapEditorCamera.ResetCamera();
                    break;
                case KeyCode.P: //player view
                    SceneView.lastActiveSceneView.orthographic = !SceneView.lastActiveSceneView.orthographic;
                    MapEditorCamera.CopyCamera();
                    break;
                case KeyCode.L: //reset view
                    SceneView.lastActiveSceneView.orthographic = false;
                    MapEditorCamera.ResetCamera();
                    break;
            }
        }
        using (new Handles.DrawingScope(Color.clear))
        {
            Vector3 newpos = Handles.FreeMoveHandle(position, new Quaternion(),0.35f,Vector3.one, Handles.SphereHandleCap);
            position = new Vector3(newpos.x,position.y,newpos.z);
        }
        if (EditorGUI.EndChangeCheck())
        {
            Vector3 delta = position - Tools.handlePosition;

            Undo.RecordObjects(Selection.transforms, "Tile Editor");

            foreach (var transform in Selection.transforms)
                transform.position += delta;
        }
    }
}
public class MapEditorCamera : EditorWindow
{ 
    [MenuItem("Pokémon Unity/Copy Player Camera %_;")]
    public static void CopyCamera()
    {
        var view = SceneView.lastActiveSceneView;
        if(view != null)
        {
            view.isRotationLocked = true;
            view.showGrid = false;
            Tools.visibleLayers = -257;
            Tools.lockedLayers = -56;
            view.drawGizmos = false;
            view.AlignViewToObject(Camera.main.transform);
            view.cameraSettings.fieldOfView = Camera.main.fieldOfView;
            view.cameraSettings.dynamicClip = false;
            view.cameraSettings.nearClip = Camera.main.nearClipPlane;
            view.cameraSettings.farClip = Camera.main.farClipPlane;
            view.cameraSettings.easingEnabled = false;
            view.cameraSettings.accelerationEnabled = false;
            view.cameraSettings.occlusionCulling = Camera.main.useOcclusionCulling;
            view.AlignViewToObject(Camera.main.transform); //twice?
            //view.LookAt(Vector3.zero);
        }
    }
    [MenuItem("Pokémon Unity/Reset Editor Camera %#;")]
    public static void ResetCamera()
    {
        var view = SceneView.lastActiveSceneView;
        if(view != null)
        {
            view.isRotationLocked = false;
            view.showGrid = true;
            Tools.visibleLayers = -1;
            Tools.lockedLayers = -312;
            view.drawGizmos = true;
            view.ResetCameraSettings();
            view.AlignViewToObject(Camera.main.transform);
        }
    }
    [MenuItem("Pokémon Unity/Reset Editor Camera %#;", true)]
    static bool ValidateReset() {
        return SceneView.lastActiveSceneView != null;
    }
    [MenuItem("Pokémon Unity/Copy Player Camera %_;", true)]
    static bool ValidateCopy() {
        return SceneView.lastActiveSceneView != null;
    }
}