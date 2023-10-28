using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVProject.MyREST
{
    [Serializable]
    public struct SDKGetPosesRequest
    {
        public static string endpoint = "getposes";
        public string token;
        public int id;
    }

    [Serializable]
    public struct SDKGetPosesReslt
    {
        public int count;
        public List<Pose> poses;
    }

    [Serializable]
    public struct Pose
    {
        public double px;
        public double py;
        public double pz;
        public double r00;
        public double r01;
        public double r02;
        public double r10;
        public double r11;
        public double r12;
        public double r20;
        public double r21;
        public double r22;
    }
}