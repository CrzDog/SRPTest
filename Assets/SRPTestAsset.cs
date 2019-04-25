using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
#endif

class SRPTestAsset : RenderPipelineAsset
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Render Pipeline/SRPTest Asset")]
    static void CreateSRPTestPipeline() {
	    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
		    0, CreateInstance<CreateSRPTestAsset>(),
		    "SRPTest.asset", null, null
	    );
    }
    class CreateSRPTestAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string path, string resourceFile)
        {
            var inst = CreateInstance<SRPTestAsset>();
            AssetDatabase.CreateAsset(inst, path);
        }
    }
#endif
    protected override IRenderPipeline InternalCreatePipeline()
    {
        return new SRPTest();
    }
}
