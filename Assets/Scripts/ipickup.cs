using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ipickup
{
    // Start is called before the first frame update
    void grab();
    void drop();
    void addrot(float rot);
    Vector3 getpos();
    void setpos(Vector3 pos);

}
