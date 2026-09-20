using UnityEngine;

[ExecuteAlways, ImageEffectAllowedInSceneView]
public class VisualEffect : MonoBehaviour
{

    public Shader Shader;
    [Range(0.0f, 1.0f)]
    public float Amount = 1.0f;
    public float Target = 1.0f;
    public float Time_ = 120.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Shader == null) Graphics.Blit(source, destination);
        Material mat = new Material(Shader);
        mat.SetFloat("_Amount", Amount);
        Graphics.Blit(source, destination, mat);
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            Amount += Time.deltaTime / Time_;
            if (Target < Amount) Amount -= Time.deltaTime;
        }
        Amount = Mathf.Clamp01(Amount);
    }

}
