using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Infrastructure")]
    [SerializeField] bool isBuildActive;
    public bool IsBuildActive { get { return isBuildActive; } set { isBuildActive = value; } }

    [SerializeField] Infrastructure newInfrastructure;
    public Infrastructure NewInfrastructure { get { return newInfrastructure; } set { newInfrastructure = value; } }

    [SerializeField] LayerMask infrastructureLayersToHit;
    public LayerMask InfrastructureLayersToHit { get { return infrastructureLayersToHit; } }
}
