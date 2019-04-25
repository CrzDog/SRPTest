using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

class SRPTestAsset : RenderPipelineAsset
{

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

    protected override IRenderPipeline InternalCreatePipeline()
    {
        return new SRPTest();
    }
}
