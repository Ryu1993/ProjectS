Singleton 사용법 : 그냥 싱글톤으로 만들고 싶으면 Monobehaviour 대신 Singleton<T> 상속
                       하이어라키에 여러개 존재하면 제일 위에 있는 놈을 반환함

ObjectPool 사용법 : 
ObjectPool로 관리하고 싶은 오브젝트가 있다면
오브젝트에 IPoolingable 인터페이스를 포함시킨후
ObjectPoolManager.Instance.PoolRequest 메서드로 해당 오브젝트의 ObjectPool 생성 후 반환받음
반환받은 ObjectPool의 Call메서드로 오브젝트 출력

ObjectPool로 출력한 오브젝트 반환 :
ObjectPool로 생성한 오브젝트에는 생성된 ObjectPool에 대한 정보가 home 변수로 남아있음
해당 오브젝트에서 home.Return(this.gameobject)로 오브젝트 풀로 반환시킬 수 있음

자세한건 스크립트 주석으로!




