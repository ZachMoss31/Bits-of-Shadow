using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMutator : MonoBehaviour
{
    //Universal Variables
    //Objects original position on start
    Vector3 tempPos;
    Vector3 tempSize;
    Quaternion tempRot;

    //Reset and Freeze


    //Vertical Hover Mutations
    public bool canHover;
    public bool canHorizHover;
    Vector3 toHover;
    public float hoverDistance = 0.5f;
    public float hoverSpeed = 0.0005f;
    bool goingUp = true;

    //Horizontal Hover Mutations
    Vector3 toHorizHover;
    bool goingSide = true;

    //Rotation Mutations
    public bool canRotate;
    Vector3 toRotate;
    public float rotationSpeed = 2f;
    public float rotX = 0f;
    public float rotY = 0f;
    public float rotZ = 0f;

    //Resize Mutations
    public bool canResize;
    bool growingUp = true;
    Vector3 toResize;
    Vector3 currGrowth;
    public float resizeSpeed = 1f;
    public float resizeAmountX = 1f;
    public float resizeAmountY = 1f;
    public float resizeAmountZ = 1f;

    private void Start()
    {
        //Set the default values...
        tempPos = new Vector3();
        tempPos = transform.position;
        tempRot = new Quaternion();
        tempRot = transform.rotation;
        tempSize = new Vector3();
        tempSize = transform.localScale;

        //Set the user values
        toHover = new Vector3(0f, hoverSpeed, 0f);
        toHorizHover = new Vector3(hoverSpeed, 0f, 0f);
        toRotate = new Vector3(rotX, rotY, rotZ);
        toResize = new Vector3(transform.localScale.x + resizeAmountX, transform.localScale.y + resizeAmountY, transform.localScale.z + resizeAmountZ);
        currGrowth = new Vector3(0.001f, 0.001f, 0.001f) * resizeSpeed;
    }

    //Update the rotation each frame by certain amounts
    void Update()
    {
        ObjRotate(toRotate);
        ObjResize(toResize);
    }

    //Call after other updates each frame and add some height or decrease some height
    private void LateUpdate()
    {
        VerticalHover(goingUp);
        HorizontalHover(goingSide);
    }

    private void VerticalHover(bool direction)
    {
        if (!canHover)
        {
            return;
        }

        if (direction)
        {
            if (transform.position.y < tempPos.y + hoverDistance)
            {
                transform.position = transform.position + toHover;
            }
            else
            {
                goingUp = false;
            }
        }
        else
        {
            if (transform.position.y > tempPos.y - hoverDistance)
            {
                transform.position = transform.position - toHover;
            }
            else
            {
                goingUp = true;
            }
        }
    }

    private void HorizontalHover(bool direction)
    {
        if (!canHorizHover)
        {
            return;
        }

        if (direction)
        {
            if (transform.position.x < tempPos.x + hoverDistance)
            {
                transform.position = transform.position + toHorizHover;
            }
            else
            {
                goingSide = false;
            }
        }
        else
        {
            if (transform.position.x > tempPos.x - hoverDistance)
            {
                transform.position = transform.position - toHorizHover;
            }
            else
            {
                goingSide = true;
            }
        }
    }

    private void ObjRotate(Vector3 rotation)
    {
        if (!canRotate)
        {
            return;
        }
        transform.Rotate(rotation * (rotationSpeed * Time.deltaTime));
    }

    private void ObjResize(Vector3 resize)
    {
        if (!canResize){
            return;
        }

        if (growingUp)
        {
            if(transform.localScale.x < resize.x)
            {
                transform.localScale += currGrowth;
            }
            else
            {
                growingUp = false;
            }
        }
        else
        {
            if(transform.localScale.x > tempSize.x)
            {
                transform.localScale -= currGrowth;
            }
            else
            {
                growingUp = true;
            }
        }
    }
}
