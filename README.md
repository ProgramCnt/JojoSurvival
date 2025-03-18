![1조 아이템 수집및 채광](https://github.com/user-attachments/assets/32273ed8-ea9b-4c26-b87c-99b72e0b9a81)# 🏆 JoJo Survial
플레이어가 자원을 수집하고 여러 아이템들을 만들며 생존을 위해 적과 싸우는 **3D 서바이벌 게임**입니다!

---

## ⚙ 주요 시스템
**🏹 전투 시스템**
- 플레이어와 NPC 몬스터들의 전투 시스템 
- 플레이어는 다양한 아이템으로 공격
- 다양한 몬스터들이 플레이어를 공격

**🎁 인벤토리 시스템**
- 보유 아이템 확인
- 장비 장착 / 해제 가능
- 소비 아이템 사용 가능

**🛒 아이템 제작 시스템**
- 플레이어는 자신이 원하는 아이템 제작
- 해당 아이템을 만드는데 필요한 재료를 모아 제작 가능 
- 소비 아이템이나 장비 같은 다양한 아이템 제작 가능

**🍎 수집 및 채광 시스템**
- 플레이어는 아이템을 만들기 위해 필요한 재료를 수집
- 나무나 돌 같은 오브젝트들을 캐면서 재료를 수집

 **🏰 건설 시스템**
- 플레이어가 들어가 쉴 수 있는 건물 제작 시스템
- 원하는 건물을 선택 후 청사진을 제작
- 해당 건물에 맞는 건물 부품을 제작
- 청사진에 부품을 모두 부착해 건물 제작

---

## 🎮 게임 플레이 가이드
**1기본 움직임**
![1조 기본 움직임 움짤](https://github.com/user-attachments/assets/428f5cba-773d-49c5-9128-4f6347191070)
- WASD 입력으로 이동
- 마우스 움직임으로 시점 변경

 **인벤토리**
- TAB 키를 눌러 인벤토리 열기
![1조 아이템 소비](https://github.com/user-attachments/assets/8a91987b-2855-444b-8199-d496debc4fbf)
![1조 아이템 장착](https://github.com/user-attachments/assets/211782f4-74b8-4415-8554-61fa546a17d3)
- 소모 아이템은 우클릭 후 소모
- 장착 아이템들은 우클릭 후 장착
- 버리고 싶은 아이템은 좌클릭후 인벤토리 밖으로 버리기

**아이템 제작**
![1조 아이템 제작](https://github.com/user-attachments/assets/b5d306a6-64dc-4b74-ac6f-a617262075de)
- 인벤토리를 열고 제작 하고 싶은 아이템 클릭
- 제작에 필요한 재료들을 모아 만들기 버튼 클릭

**아이템 수집 및 채광**
- ![1조 아이템 수집및 채광](https://github.com/user-attachments/assets/5e12361c-3931-4c6d-8376-6baf3f10ade0)
- 수집하고 싶은 아이템을 보고 E키를 입력해 수집
- 채광에 필요한 아이템을 장착 후 오브젝트를 향해 마우스 왼쪽 클릭
  
**건물 건설**
![1조 청사진 부착](https://github.com/user-attachments/assets/164053ec-209e-4050-9f4d-bbd5559e9694)
![1조 부품 부착](https://github.com/user-attachments/assets/598bab3a-5e1c-4c5b-a286-4c546f3e96b6)
![1조 집 완성](https://github.com/user-attachments/assets/d171556d-31e4-4151-8050-b36921932ef8)
- 원하는 건물 청사진 제작
- 청사진을 장착 후 원하는 땅에 마우스 왼쪽 클릭을 해서 부착
- 해당 건물에 들어가는 부품들에 필요한 아이템 수집 후 제작
- 부품을 장착 후 땅에 부착한 건물의 청사진을 바라보고 왼쪽 클릭 해서 부품 부착
- 모든 부품을 부착해 건물 완성

---

## 📂 스크립트 구조
📦 Scripts

 ┣ 📂 Craft          # 아이템 제작 관련 코드

 ┃ ┣ 📜 CraftingManager.cs        # 아이템 제작 매니저

 ┃ ┣ 📜 CraftingRecipe.cs           # 아이템 제작 레시피
 
 ┣ 📂 Enviroment                 # 환경 관련 코드

 ┃ ┣ 📂 MapObject                # 돌, 나무 같은 오브젝트

 ┃ ┃ ┣ 📜 EnvironmentObject.cs             # 환경 오브젝트 클래스

 ┃ ┣ 📂 ResourceSpawner               # 환경 생성 관련 코드

 ┃ ┃ ┣ 📜 ResourceSpawner.cs      # 환경 생성 코드
 
 ┃ ┃ ┣ 📜 ResourceType.cs      # 환경 데이터

 ┣ 📂 Interface            # 인터페이스

 ┃ ┣ 📜 Interface.cs             # NPC 관련 인터페이스

 ┣ 📂 Item    # 아이템 관련 코드

 ┃ ┣ 📜 AttackItem.cs     # 공격 아이템 
 
 ┃ ┣ 📜 BuildableItem.cs     # 건물 청사진

 ┃ ┣ 📜 BuildablePartsItem.cs      # 건물 부품

 ┃ ┣ 📜 BuildBouse.cs   # 건물

 ┃ ┣ 📜 CraftItem.cs        # 제작 아이템

 ┃ ┣ 📜 Entity.cs  # 환경 아이템 엔티티

 ┃ ┣ 📜 Equip.cs        # 제작 아이템

 ┃ ┣ 📜 Item.cs        # 기본 아이템

 ┃ ┣ 📜 ItemData.cs        # 아이템 데이터 
 
 ┃ ┣ 📜 ItemDataEditor.cs        # 아이템 데이터 에디터

 ┣ 📂 Manager    # 각종 매니저

 ┃ ┣ 📜 UIManager.cs        # UI 매니저

 ┣ 📂 NPC    # NPC 몬스터 관련 코드 
 
 ┃ ┣ 📂 States    # NPC 상태 관련 코드
  
 ┃ ┃ ┣ 📜 NPCAttackState.cs        # NPC 공격 상태 

 ┃ ┃ ┣ 📜 NPCChaseState.cs        # NPC 추적 상태 

 ┃ ┃ ┣ 📜 NPCFleeState.cs        # NPC 도망 상태 

 ┃ ┃ ┣ 📜 NPCIdle.cs        # NPC 기본 상태 

 ┃ ┃ ┣ 📜 NPCWander.cs        # NPC 순회 상태 

 ┃ ┣ 📜 Detect.cs        # NPC 감지 코드

 ┃ ┣ 📜 NPCCombat.cs        # NPC 공격 코드

 ┃ ┣ 📜 NPCStateController.cs        # NPC 상태 머신
 
 ┣ 📂 Player    # 플레이어 관련 코드 
 
 ┃ ┣ 📜 Equipment.cs        # 아이템 장착

 ┃ ┣ 📜 Interation.cs  # 상호작용 
 
 ┃ ┣ 📜 Player.cs        # 플레이어

 ┃ ┣ 📜 PlayerCondition.cs        # 상태

 ┃ ┣ 📜 PlayerController.cs        # 컨트롤러

 ┣ 📂 UI    # UI 관련 코드
 
 ┃ ┣ 📜 Condition.cs        # 상태 UI

 ┃ ┣ 📜 DamageIndicator.cs  # 데미지 상기
 
 ┃ ┣ 📜 DayNightCycle.cs        # 시간 

 ┃ ┣ 📜 IngredientItem.cs        # 제작 재료

 ┃ ┣ 📜 Inventory.cs        # 인벤토리

 ┃ ┣ 📜 ItemSlot.cs        # 아이템 슬롯

 ┃ ┣ 📜 ItemToolTip.cs  # 아이템 사용팁
 
 ┃ ┣ 📜 UICondition.cs        # 상태 관리

 ┃ ┣ 📜 UICraftItem.cs        # 아이템 제작 

 ┃ ┣ 📜 UIManager.cs        # UI 매니저

