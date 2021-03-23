/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   EventType.cs                  
ProductionDate :  2017-12-08 11:24:08
Author         :   T-CODE
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroLight 
{
    public enum EventType
    {
        EnActive,
        DisActive,
        CloseFull,
        OpenFull,
        SwipingDown,
        SwipingUp,
        SwipingLeft,
        SwipingRight,
        ThumbAndIndexUp,
        SayYes,
        FaceUp,
        ThreeFinger,
        IndexFinger,
        ShowUI,
    }
}
