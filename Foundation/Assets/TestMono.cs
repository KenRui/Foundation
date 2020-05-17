using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestMono : MonoBehaviour
{
    public Material mat;
    private CommandBuffer cb;
    private Renderer render;
    private SkinnedMeshRenderer sRender;
    private void Awake()
    {
        cb = new CommandBuffer();
        sRender = this.GetComponent<SkinnedMeshRenderer>();
        render = sRender;

    }

    void Start()
    {
        // StartCoroutine(new Coroutine())
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWillRenderObject()
    {
        // cb.Clear();
        // cb.DrawRenderer(render, mat);
        // Graphics.ExecuteCommandBuffer(cb);
    }

    private void OnRenderObject()
    {
        cb.Clear();
        cb.DrawMesh(sRender.sharedMesh, sRender.localToWorldMatrix, mat);
        Graphics.ExecuteCommandBuffer(cb);
    }
}
