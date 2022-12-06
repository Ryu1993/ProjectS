using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;

namespace BC
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class SceneSlime : MonoBehaviour, IInteraction, IItemable
    {

        public Slime curSlime;
        private float hungry;
        private UnityAction stateAction;
        private Animator animator;
        private readonly int animationSpeed = Animator.StringToHash("Speed");
        private readonly int animationJump = Animator.StringToHash("Jump");
        public float Hungry { get { return hungry; }
            set
            {
                hungry = value;
                if(hungry > 300)
                {
                    FindFeed();
                }
            }
        }
        private float speed;
        private float jumpPower;

        private WaitForSecondsRealtime hungryTime;

        public float size;

        private Coroutine hungryCheck;
        private Rigidbody slimeBody;
        private NavMeshAgent agent;

        [SerializeField]
        LayerMask groundMask;
        RaycastHit hit;
        [SerializeField]
        Transform raycastOrigin;
        private Collider[] colliders = new Collider[1];
        LayerMask feedMask;

        float idleCount;

        private void OnEnable()
        {
            Hungry = curSlime.hungry;
            speed = curSlime.speed;
            jumpPower = curSlime.jumpPower;

            slimeBody = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            //���� �߰�
        }
        private void FixedUpdate()
        {
            stateAction?.Invoke();
            Hungry += 0.02f;
            animator.SetFloat(animationSpeed, agent.velocity.magnitude);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                idleCount += Time.fixedDeltaTime;
                if(idleCount>5f)
                {
                    SlimeMovement();
                    idleCount = 0;
                }
                else
                {
                    SlimeJump();
                }
            }
        }
        public void BeingItme()
        {
            //�������� �Ǿ������
            //Ǯ���� ��Ȱ��ȭ��Ű��
        }
        public Item ItemRequest()
        {
            throw new System.NotImplementedException();
        }


        public void ItemReturn()
        {
            
        }

        private void SlimeMovement()
        {
            raycastOrigin.localRotation = Quaternion.Euler(new Vector3(10, Random.Range(0, 360f), 0));
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward*20);
            if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, Mathf.Infinity, groundMask))
            {
                agent.SetDestination(hit.point);
            }
        }

        private void FindFeed()
        {
            colliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position, 1f, colliders, feedMask);
            if(colliders[0] != null)
            {
                if (colliders[0].TryGetComponent(out IEatable feed))
                {
                    if(feed.CropRequest()==curSlime.likeFeed)
                    {
                        feed.home.Return(colliders[0].gameObject);
                        hungry += 150;
                        CreateGem();
                    }
                }
            }
        }

        private void CreateGem()
        {
            //���� �ۼ�
        }

        private void SlimeJump()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash != animationJump)
            {
                if (Random.Range(0, 100) > 98)
                {
                    animator.SetTrigger(animationJump);
                }
            }
        }

    }
}

