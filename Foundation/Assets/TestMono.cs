using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestMono : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    private CommandBuffer cb;

    private Renderer render;
    public Material defaultMat;
    private void Awake()
    {
        cb = new CommandBuffer();
        render = this.GetComponent<SkinnedMeshRenderer>();

    }

    void Start()
    {
        
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
        cb.DrawRenderer(render, mat);
        cb.DrawRenderer(render, defaultMat);
        Graphics.ExecuteCommandBuffer(cb);
    }
}
