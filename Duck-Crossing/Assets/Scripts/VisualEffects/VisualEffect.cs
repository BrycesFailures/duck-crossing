using UnityEngine;

[ExecuteAlways, ImageEffectAllowedInSceneView]
public class VisualEffect : MonoBehaviour
{

    public Shader Shader;
    [Range(0.0f, 1.0f)]
    public float Amount = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Shader == null) Graphics.Blit(source, destination);
        Material mat = new Material(Shader);
        mat.SetFloat("_Amount", Amount);
        Graphics.Blit(source, destination, mat);
    }

}
