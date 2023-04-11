﻿using System;

[Flags]
public enum MenuUiState : int
{
    noneState = 0, 
    informationState = 1, 
    infrastructureInformationState = 2,
    infrastructureState = 4,
    infrastructureManageState = 8,
    infrastructureAboutState = 16,
    infrastructureBuildState = 32,
    advisorState = 64,
    advisorKingState = 128, 
    advisorJokerState = 256, 
    advisorKnightState = 512, 
    advisorExecutionerState = 1024
}
