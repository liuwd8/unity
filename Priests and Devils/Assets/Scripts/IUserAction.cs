using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction {
    void UserClick(GameObject target);
    void Restart();
    void Pause();
}
