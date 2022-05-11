using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character
{
    [SerializeField] GameObject BladePrefab;
    [SerializeField] GameObject ArrowPrefab;

    List<GameObject> Blades;
    List<GameObject> Arrows;

    protected override void Awake()
    {
        base.Awake();
        Blades = new List<GameObject>();
        Arrows = new List<GameObject>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AttackCoroutine = BowAttack;
        MaxHp = 100f;
        curHp = 100f;
        Power = 10f;
        Defensive = 5f;
        MagicResist = 5f;

        HpBar.GetComponent<StateBar>().Activate(curHp / MaxHp);
    }


    IEnumerator SwordAttack()
    {
        while(true)
        {
            yield return null;
        }
    }

    IEnumerator BowAttack()
    {
        GameObject target = null;

        foreach(var obj in Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy")))
        {

            if (target == null)
                target = obj.gameObject;
            else
            {
                if (Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, obj.transform.position))
                    target = obj.gameObject;
            }
        }

        float gauge = 0.5f;
        while (attackOrder)
        {
            if (gauge < 1.5f)
                gauge += Time.deltaTime * GameManager.Instance.TimeScale * 0.5f;
            else
                gauge = 1.5f;

            if(target != null)
            {
                float angle = Vector3.SignedAngle(Vector3.forward, target.transform.position - transform.position, Vector3.up);
                if (angle >= -45f && angle <= 45f)
                    Turn(DIRECT.DIR_BACK);
                else if (angle < -45f && angle > -135f)
                    Turn(DIRECT.DIR_LEFT);
                else if (angle > 45f && angle < 135f)
                    Turn(DIRECT.DIR_RIGHT);
                else if (angle <= -135f || angle >= 135f)
                    Turn(DIRECT.DIR_FRONT);
            }

            Move(direction * DefaultSpeed * slowScale);
            yield return null;
        }

        GameObject arrow = GenerateArrow();

        arrow.transform.position = transform.position;
        Vector3 dirVec = Vector3.zero;
        if(target == null)
        {
            if (curDir == DIRECT.DIR_LEFT)
                dirVec = Vector3.left;
            else if (curDir == DIRECT.DIR_RIGHT)
                dirVec = Vector3.right;
            else if (curDir == DIRECT.DIR_FRONT)
                dirVec = Vector3.down;
            else if (curDir == DIRECT.DIR_BACK)
                dirVec = Vector3.up;
        }
        else
            dirVec = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z, 0f).normalized;

        arrow.GetComponent<Arrow>().inputInformation(dirVec, gameObject, Power * gauge);
    }

    GameObject GenerateArrow()
    {
        foreach (var arrow in Arrows)
        {
            if (!arrow.activeSelf)
            {
                arrow.SetActive(true);
                return arrow;
            }
        }

        GameObject prefab = Instantiate(ArrowPrefab);

        Arrows.Add(prefab);
        return prefab;
    }
}
