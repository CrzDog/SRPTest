using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;


public class SRPTest : RenderPipeline
{
    CommandBuffer _cb;

    public override void Dispose()
    {
        base.Dispose();

        if (_cb != null)
        {
            _cb.Dispose();
            _cb = null;
        }
    }

    public override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        base.Render(context, cameras);

        if (_cb == null)
        {
            _cb = new CommandBuffer();
        }

        var rtDesc = new RenderTextureDescriptor(1024, 1024, RenderTextureFormat.Default, 24);
        var rtID = Shader.PropertyToID("_TempRT");

        foreach (var camera in cameras)
        {

            context.SetupCameraProperties(camera);
            _cb.name = "Setup";
            _cb.GetTemporaryRT(rtID, rtDesc);
            _cb.SetRenderTarget(rtID);
            _cb.ClearRenderTarget(true, true, camera.backgroundColor);
            context.ExecuteCommandBuffer(_cb);
            _cb.Clear();

            var culled = new CullResults();
            CullResults.Cull(camera, context, out culled);

            var settings = new DrawRendererSettings(camera, new ShaderPassName("Base"));
            var filter = new FilterRenderersSettings(true);
            filter.renderQueueRange = RenderQueueRange.opaque;
            context.DrawRenderers(culled.visibleRenderers, ref settings, filter);

            _cb.name = "Blit";
            _cb.Blit(rtID, BuiltinRenderTextureType.CameraTarget);
            context.ExecuteCommandBuffer(_cb);
            _cb.Clear();

            context.Submit();
        }
    }
}

