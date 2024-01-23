using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchorMeshRenderTail : MonoBehaviour
{
    public APlayer aplayer;
    public float activeTime = 0.5f;
    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public Transform positionToSpawn;
    public float meshDestroyDelay;

    [Header("Shader Related")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.0f;

    private bool isTrailActive;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    void Start()
    {
        aplayer = GetComponent<APlayer>();
    }


    void Update()
    {
        if (aplayer.desh)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isTrailActive = true;
                StartCoroutine(ActivateTrail(activeTime));
            }
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject objs = new GameObject ();
                objs.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation); ;

                MeshRenderer mr = objs.AddComponent<MeshRenderer>();
                MeshFilter mf = objs.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh(); 
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AniamteMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(objs, meshDestroyDelay);

            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
        aplayer.desh = false;
        aplayer.isDeshInvincible = false;
    }

    IEnumerator AniamteMaterialFloat(Material mat, float goal, float rate, float refrehRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refrehRate);
        }
    }
}
