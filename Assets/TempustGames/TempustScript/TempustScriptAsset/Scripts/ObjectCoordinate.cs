using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace TempustScript
{
    [DataContract(Name = "ObjectCoordinate")]
    public class ObjectCoordinate
    {
        [DataMember] private Coordinate x;
        [DataMember] private Coordinate y;
        [DataMember] private Coordinate z;
        [DataMember] private TSScript parent;

        public ObjectCoordinate(TSScript parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Get the coordinates as a Vector3.
        /// </summary>
        /// <param name="obj">The object whose coordinates are being changed, used as a default for axes not set.</param>
        /// <param name="defaultObj">If true, then any coordinates not specified are taken from "obj". If false, default to 0.</param>
        /// <returns>A Vector3 representation of the object</returns>
        public Vector3 GetVector(string obj, bool defaultObj=false)
        {
            Transform objTransform = parent.GetObject(obj).transform;
            Vector3 pos = new Vector3();

            if (x != null)
            {
                pos.x = x.coord + (x.isRelative ? parent.GetObject(x.relativeObj).transform.position.x : 0);
            }
            else if (defaultObj)
            {
                pos.x = objTransform.position.x;
            }

            if (y != null)
            {
                pos.y = y.coord + (y.isRelative ? parent.GetObject(y.relativeObj).transform.position.y : 0);
            }
            else if (defaultObj)
            {
                pos.y = objTransform.position.y;
            }

            if (z != null)
            {
                pos.z = z.coord + (z.isRelative ? parent.GetObject(z.relativeObj).transform.position.z : 0);
            }
            else if (defaultObj)
            {
                pos.z = objTransform.position.z;
            }

            return pos;
        }

        ///<summary>Set the coordinate this command sets. Only call this during script compiling</summary>
        public void SetXCoord(float coord, bool relative = false, string relativeObj = "")
        {
            x = new Coordinate(coord, relative, relativeObj);
        }

        ///<summary>Set the coordinate this command sets. Only call this during script compiling</summary>
        public void SetYCoord(float coord, bool relative = false, string relativeObj = "")
        {
            y = new Coordinate(coord, relative, relativeObj);
        }

        ///<summary>Set the coordinate this command sets. Only call this during script compiling</summary>
        public void SetZCoord(float coord, bool relative = false, string relativeObj = "")
        {
            z = new Coordinate(coord, relative, relativeObj);
        }

        [DataContract(Name = "Coordinate")]
        private class Coordinate
        {
            [DataMember] public bool isRelative { get; private set; }
            [DataMember] public string relativeObj { get; private set; }
            [DataMember] public float coord { get; private set; }


            public Coordinate(float coord, bool isRelative, string relativeObj)
            {
                this.isRelative = isRelative;
                this.relativeObj = relativeObj;
                this.coord = coord;
            }
        }
    }
}
