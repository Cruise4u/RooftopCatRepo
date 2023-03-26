using System.Collections;
using UnityEngine;

public class FSM<T> : MonoBehaviour
{
    private T owner;
    private State<T> currentState;
    private State<T> previousState;
    private State<T> globalState;

    public FSM(T owner)
    {
        this.owner = owner;
    }

    public void Update()
    {
        if (globalState != null)
        {

        }

        if (currentState != null)
        {

        }
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null)
        {
            Debug.LogError("Null state error");
            return;
        }

        previousState = currentState;
        if (currentState != null)
        {
            currentState.ExitState(owner);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.EnterState(owner);
        }
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public void SetGlobalState(State<T> state)
    {

    }
}
