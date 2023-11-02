using UnityEngine;

public class ParticleDrawing : MonoBehaviour {
    private Texture2D workingTexture;
    private Renderer mioRenderer;
    Texture2D texture;
    
    void Start () {
        mioRenderer = GetComponent<Renderer> ();
        texture = mioRenderer.material.mainTexture as Texture2D;
        workingTexture = new Texture2D (texture.width, texture.height);
        Color32 baseColor = new Color32 (255,255,0,255);
        Color32[] sourcePixels = texture.GetPixels32();
        workingTexture.SetPixels32( sourcePixels );
        workingTexture.Apply();
        mioRenderer.material.mainTexture = workingTexture;
    }
    
    void OnParticleCollision(GameObject other) {
        int num = other.GetComponent<ParticleSystem>().GetSafeCollisionEventSize();
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[num];
        int result = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
        Color32 pixelColor = new Color32 (255,255,0,255);

        for (int i=0; i<num; i++) {
            //draw a pixel at the uv location of the collision
            RaycastHit hit;
            Vector3 pos = Vector3.zero;
            Vector2 pixelUV;
            Vector2 pixelPoint;

            if (Physics.Raycast (collisionEvents [i].intersection, -Vector3.up, out hit)) {
                pos = hit.point;
                pixelUV = hit.textureCoord;
                pixelPoint = new Vector2(pixelUV.x * texture.width,pixelUV.y * texture.height);
                if (pixelPoint.x > 0){
                    if (pixelPoint.y > 0)
                        workingTexture.SetPixel((int) pixelPoint.x - 1,(int)pixelPoint.y - 1,Color.yellow);
                        workingTexture.SetPixel((int) pixelPoint.x - 1,(int)pixelPoint.y,Color.yellow);
                    if (pixelPoint.y < texture.height - 1)
                        workingTexture.SetPixel((int) pixelPoint.x - 1,(int)pixelPoint.y + 1,Color.yellow);
                }

                if (pixelPoint.y > 0)
                    workingTexture.SetPixel((int) pixelPoint.x,(int)pixelPoint.y - 1,Color.yellow);
                    workingTexture.SetPixel((int) pixelPoint.x,(int)pixelPoint.y,Color.yellow);
                if (pixelPoint.y < texture.height - 1)
                    workingTexture.SetPixel((int) pixelPoint.x,(int)pixelPoint.y + 1,Color.yellow);

                if (pixelPoint.x < texture.width - 1){
                    if (pixelPoint.y > 0)
                        workingTexture.SetPixel((int) pixelPoint.x + 1,(int)pixelPoint.y - 1,Color.yellow);
                        workingTexture.SetPixel((int) pixelPoint.x + 1,(int)pixelPoint.y,Color.yellow);
                    if (pixelPoint.y < texture.height - 1)
                        workingTexture.SetPixel((int) pixelPoint.x + 1,(int)pixelPoint.y + 1,Color.yellow);
                }
            }
        }
        workingTexture.Apply();
    }
}
