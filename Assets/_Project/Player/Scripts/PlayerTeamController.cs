using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    public void SetMesh(Mesh mesh)
    {
        meshRenderer.sharedMesh = mesh;
    }
}
