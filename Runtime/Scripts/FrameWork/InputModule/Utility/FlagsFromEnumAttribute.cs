/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   FlagsFromEnumAttribute.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System;
using UnityEngine;

namespace MicroLight.UnityPlugin.Utility
{
    public class FlagsFromEnumAttribute : PropertyAttribute
    {
        public Type EnumType { get; private set; }

        public FlagsFromEnumAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}