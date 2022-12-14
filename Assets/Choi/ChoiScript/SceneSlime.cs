using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;
using TMPro;

namespace BC
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class SceneSlime : MonoBehaviour, IInteraction, IItemable,IPoolingable
    {
        public Slime curSlime;
        private Animator animator;
        private SkinnedMeshRenderer sliemSkin;
        private NavMeshAgent agent;
        public NavMeshAgent Agent { get { return agent; } }
        public Rigidbody rigi { get; set; }
        public ObjectPool home { get; set; }
        public Item.ItemType type { get { return curSlime.type; }}
        [SerializeField]
        Face slimeFace;
        [SerializeField]
        private LayerMask groundMask;
        private RaycastHit hit;
        [SerializeField]
        private Transform raycastOrigin;
        [SerializeField]
        private LayerMask feedMask;
        [SerializeField]
        private LayerMask vacuumMask;
        private Collider[] feedColliders = new Collider[1];
        private Collider[] vacuumColliders = new Collider[1];
        private float idleCount;
        private bool isFlying;
        private float hungry;
        private float agentDelay;
        private float agentDelayCount;
        private UnityAction<float> vaccumCheck;
        public UnityAction returnAcition;
        private NavMeshHit navHit;
        private MeshFilter hatMeshFilter;
        private MeshRenderer hatMeshRender;
        private void Awake()
        {
            rigi = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            sliemSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void HatSetting(Vector3 position,Vector3 rotation)
        {
            Transform hat = transform.GetChild(0).GetChild(0).GetChild(0);
            hat.TryGetComponent(out hatMeshFilter);
            hat.TryGetComponent(out hatMeshRender);
            hat.localPosition = position;
            hat.localRotation = Quaternion.Euler(rotation);
        }

        public void SlimeSet(Slime slime)
        {
            curSlime = slime;
            sliemSkin.sharedMesh = slime.itemMesh;
            sliemSkin.sharedMaterials[0] = slime.itemMaterilal;
            HatSetting(curSlime.hatPosition, curSlime.hatRotation);
            if(slime.isHat)
            {
                hatMeshFilter.sharedMesh = slime.hatMesh;
                hatMeshRender.sharedMaterial = slime.hatMaterial;
            }
            FaceSet(slimeFace.Idleface);
            hungry = 0;
            agent.enabled = true;
            agent.speed = curSlime.speed;
            isFlying = false;
        }
        public void FaceSet(Texture tex)
        {
            sliemSkin.materials[1].SetTexture(Parameter.mainTex, tex);
        }
        private void FixedUpdate()
        {
            vaccumCheck?.Invoke(agentDelay);
            if (isFlying) return;
            hungry += Time.fixedDeltaTime;
            if (hungry <= 300)
            {
                FindFeed();
            }
            if(hungry>300)
            {
                CreateGem();
            }
            animator.SetFloat(Parameter.speed, agent.velocity.magnitude);
            if (agent.remainingDistance <= agent.stoppingDistance)
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
            FaceSet(slimeFace.jumpFace);
            raycastOrigin.localRotation = Quaternion.Euler(new Vector3(10, Random.Range(0, 360f), 0));
            if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, Mathf.Infinity, groundMask))
            {
                agent.SetDestination(hit.point);
            }
        }

        private void FindFeed()
        {
            feedColliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position, 3f, feedColliders, feedMask);
            if(feedColliders[0] != null)
            {
                if (feedColliders[0].TryGetComponent(out SceneCrop feed))
                {
                    if(feed.Crop==curSlime.likeFeed)
                    {
                        feed.ItemReturn();
                        hungry += 150;
                    }
                }
            }
        }
        private void CreateGem()
        {
            ItemManager.Instance.CreateSceneItem(curSlime.rewardGem, transform.position);
            hungry = 0;
        }
        private void SlimeJump()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash != Parameter.jump)
            {
                if (Random.Range(0, 100) > 98)
                {
                    animator.SetTrigger(Parameter.jump);
                    FaceSet(slimeFace.jumpFace);
                }
            }
        }

        public Item ItemRequest()
        {
            return curSlime;
        }

        public void ItemReturn()
        {
            vaccumCheck = null;
            hatMeshFilter.sharedMesh = null;
            hatMeshRender.sharedMaterial = null;
            agent.enabled = false;
            animator.applyRootMotion = true;
            rigi.isKinematic = true;
            isFlying = true;
            returnAcition?.Invoke();
            home.Return(this.gameObject);
        }

        public void MoveStop()=> MoveStop(0);
        public void MoveStop(float delay)
        {
            isFlying = true;
            rigi.isKinematic = false;
            animator.applyRootMotion = false;
            agent.enabled = false;
            agentDelay = delay;
            vaccumCheck = VacuumCheck;
        }


        private void VacuumCheck(float delay)
        {
            vacuumColliders[0] = null;
            Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, 0.27f, 0), 0.35f, vacuumColliders, vacuumMask, QueryTriggerInteraction.Collide);
            if (vacuumColliders[0] != null)
            {
                transform.parent = null;
                return;
            }
            if (delay != 0)
            {
                agentDelayCount += Time.fixedDeltaTime;
                if(agentDelayCount<agentDelay)
                {
                    return;
                }
                else
                {
                    agentDelayCount = 0;
                    agentDelay = 0;
                }
            }
            if (!NavMesh.SamplePosition(transform.position,out navHit,0.3f,NavMesh.AllAreas)) return;
            rigi.isKinematic = true;
            agent.enabled = true;
            animator.applyRootMotion = true;
            isFlying = false;
            vaccumCheck = null;
        }

    }
}

