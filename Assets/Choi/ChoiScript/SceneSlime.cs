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
                if(value > 600)
                {
                    value = 600;
                }
                hungry = value;
                if(hungry > 300)
                {
                    FindFeed();
                }
            }
        }
        private float speed;
        private float jumpPower;

        public float size;

        private Coroutine hungryCheck;
        private Rigidbody slimeBody;
        private NavMeshAgent agent;

        [SerializeField]
        LayerMask groundMask;
        RaycastHit hit;
        [SerializeField]
        Transform raycastOrigin;
        private Collider[] FeedColliders = new Collider[1];
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
            //외형 추가
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
            FeedColliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position, 1f, FeedColliders, feedMask);
            if(FeedColliders[0] != null)
            {
                if (FeedColliders[0].TryGetComponent(out IEatable feed))
                {
                    if(feed.CropRequest()==curSlime.likeFeed)
                    {
                        feed.home.Return(FeedColliders[0].gameObject);
                        hungry += 150;
                        CreateGem();
                    }
                }
            }
        }

        private void CreateGem()
        {
            ItemManager.Instance.CreateSceneItem(curSlime.rewardGem, transform.position);
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

        public Item ItemRequest()
        {
            return curSlime;
        }

        public void ItemReturn()
        {
            throw new System.NotImplementedException();
        }
    }
}

