using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class ClampBounds : MonoBehaviour
    {
        private void Update()
        {
            var pos = transform.localPosition;
            if (pos.x <= 0f)
            {
                transform.localPosition += new Vector3(Consts.width, 0f, 0f);
            }
            if (pos.x > Consts.width)
            {
                transform.localPosition += new Vector3(-Consts.width, 0f, 0f);
            }
            if (pos.y <= 0f)
            {
                transform.localPosition += new Vector3(0f, Consts.height, 0f);
            }
            if (pos.y > Consts.height)
            {
                transform.localPosition += new Vector3(0f, -Consts.height, 0f);
            }

        }
    }
}