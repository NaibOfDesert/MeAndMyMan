public enum MenuUiState
{
    noneState, 
    infrastructureManageState,
    infrastructureAboutState,
    infrastructureBuildState,
    infrastructureState = infrastructureAboutState | infrastructureManageState | infrastructureBuildState, 
    advisorKingState, 
    advisorJokerState, 
    advisorKnightState, 
    advisorExecutionerState,
    advisorState = advisorKingState | advisorJokerState | advisorKnightState | advisorExecutionerState






}

