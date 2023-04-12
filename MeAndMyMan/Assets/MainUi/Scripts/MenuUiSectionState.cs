using System;

[Flags]
public enum MenuUiSectionState : Int32
{
    noneState = 0, 
    informationState = 1, 
    infrastructureInformationState = 2,
    infrastructureState = 4,
    infrastructureCreateState = 8,
    infrastructureAboutState = 16,
    infrastructureBuildState = 32,
    advisorState = 64,
    advisorKingState = 128, 
    advisorJokerState = 256, 
    advisorKnightState = 512, 
    advisorExecutionerState = 1024
}

