using System;
using System.IO;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUiMgr : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public static GameUiMgr single;
    private GameObject scanObject;//상호작용중인 오브젝트의 정보를 받아오 변수, PlayerAction에서 스캔할 오브젝트정보를 받아오기때문에 얘는 인스펙터에서 안 보여도된다.
    
    [Header("TalkPanel")]
    [SerializeField] private Image imgTalkPnel;// 대화창 표시유무를 위해 변수 선언
    public bool isActionTalk;// 대화창의 활성화 유무를 판별하기위한 변수
    public Image imgPortrait;// 초상화 이미지를 관리할 변수
    [Header("TalkMgr")]
    public TalkMgr talkMgr;// 대화 매니저를 변수로 선언하여 대화매니저의 함수에 접근 할 수 있게함.
    public int talkIndex;

    [Header("TextEffect")]
    public TypeEffect typeTextEffect;

    [Header("Player DESC")]
    //GameMgr.PlayerData.NAME/Level 을 인계받아 화면에 출력할 TMP변수
    public TextMeshProUGUI tmp_PlayerName;
    public TextMeshProUGUI tmp_PlayerLevle;
    public TextMeshProUGUI tmp_PlayerGold;

    public Slider s_HP;
    public Slider s_SN;
    public Slider s_EXP;

    [Header("Button")]
    public GameObject mainButton;// 클릭할 메인 버튼
    public GameObject[] subButtons;// 클릭하면 펼쳐질 서브버튼들.
    private bool areSubButtonsVisible = false;// 메인버튼을 클릭해서 얘가 true가 되면 서브버튼이 보여짐 
    [SerializeField] private Image img_Portrait;
    [SerializeField] private Sprite[] ary_sp_Portrait;
    [SerializeField] private GameObject objSubButtonFrame;
    
    [Header("VideoOption")]
    [SerializeField] private VideoOption videoOption_S1;

    //Minimap
    private bool bigMinimapChek;
    [Header("Minimap")]
    [SerializeField] private Camera miniCamera;
    [SerializeField] private RenderTexture minimapRanderTexture;
    [SerializeField] private RawImage rimgSmall;
    [SerializeField] private RawImage rimgBig;
    [SerializeField] private Image smallMap;
    [SerializeField] private Image bigMap;

    //Player Desc
    [Header("Desc_Player")]
    [SerializeField] private Image PlayerDesc;
    private bool DescCheck;

    //MenuSet
    [Header("Mnue Set")]
    public GameObject menuSet;

    [Header("Player Options")]
    public GameObject player;

    [Header("Quest Manager")]
    public QuestMgr questMgr;//퀘스트 번호를 가져올 퀘스트 매니저 변수 생성
    public TextMeshProUGUI questDesc;

    [Header("Invnetory")]
    [SerializeField] private GameObject inventory_panel;
    private Inventory inventory;
    [HideInInspector] public bool activeInventory = false;
    //03-31 variable Inventoty - try.4LocalDataStore
    public Slot[] slots;
    public Transform slotHolder;
    //04-21 Inventory Slot Drag items
    public Image dragIcon;
    public Slot nowSlot;
    public Slot[] targetSlots;
    public int dragIndex;
    //04-22
    public Image addEquipPanel;
    public Button btn_YesEquipAdd;
    public Button btn_NoEquipAdd;
    public bool equipmnet;


    [Header("ToolTip")]
    public GameObject tooltip;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textDesc;
    //public TextMeshProUGUI textPower;
    public Image imgIcon;
    public Canvas canvas_Tooltip;

    private float canvaseWidth;
    private RectTransform tooltipRect;


    //Vector3 lodingPosition;// player Position

    [Header("player State")]
    public float player_Max_HP;
    public float player_Cur_HP;
    public float player_Max_SN;
    public float player_Cur_SN;
    private float player_Max_MP;
    private float player_Cur_MP;

    public int playerGold;
    public int playerLevel;

    private float player_Atk_Speed;
    private float player_Atk_Range;
    private float player_Base_Atk_Dmg;
    private float player_Max_EXP;
    private float player_Cur_EXP;

    private void Awake()
    {
        single = this;
    }
    public void AddItemTest()
    {
        Debug.Log("AddItem");

        Item newItem = ItemResources.instance.itemRS[UnityEngine.Random.Range(1,6)]; // 새로운 아이템 생성
        inventory.AddItem(newItem); // 인벤토리에 아이템 추가,
        RedrawSlotUI();
    }
    public void ValueUpdate()
    {
        int ran = UnityEngine.Random.Range(0, 2);
        if (ran == 0)
        {
            playerGold += 15;
            player_Max_HP += 15;
            player_Max_SN += 15;
            player_Cur_EXP += 2;

            Debug.Log("player gold: " + playerGold);
            Debug.Log("player max hp: " + player_Max_HP);
            Debug.Log("player max sn: " + player_Max_SN);
            Debug.Log("player cur sn: " + player_Cur_EXP);
        }
        else
        {
            playerGold -= 1;
            player_Cur_HP -= 1;
            player_Cur_SN -= 1;

            Debug.Log("player gold: " + playerGold);
            Debug.Log("player cur hp: " + player_Cur_HP);
            Debug.Log("player cur sn: " + player_Cur_SN);
        }

        SliderChange();
    }
    

    private void Start()
    {

        imgTalkPnel.gameObject.SetActive(false);// NPC대화창 시작할때 꺼줌
        objSubButtonFrame.SetActive(true);//서브버튼 목록 시작할때 켜줌
        HideSubButtons();//서브버튼 하위 목록 시작할때 꺼줌
        OffVideoOption_S1();//게임옵션 설정창 시작할때 꺼줌

        bigMinimapChek = true;
        smallMap.gameObject.SetActive(true);
        bigMap.gameObject.SetActive(false);

        PlayerDesc.gameObject.SetActive(true);
        DescCheck = true;


        //03-31 Start Inventory - try.4
        inventory = Inventory.single;

        inventory_panel.SetActive(activeInventory);
        inventory.onSlotCountChange += SlotChange;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory.onChangeItem += RedrawSlotUI;

        AddSlot();//인벤토리 칸 세팅할때 나는 설정 안 만져서 그런지 이걸로 인벤토리 한번 활성화 시켜주지않으면 이상하게 동작하는거 확인.

        for (int i = 0; i < targetSlots.Length; i++)
        {
            switch (i)
            {
                case 0:
                    targetSlots[i].item.itemType = Item.ItemType.Equipment_Helmet;
                    break;
                case 1:
                    targetSlots[i].item.itemType = Item.ItemType.Equipment_Arrmor;
                    break;
                case 2:
                    targetSlots[i].item.itemType = Item.ItemType.Equipment_Weapon;
                    break;
                case 3:
                    targetSlots[i].item.itemType = Item.ItemType.Equipment_Boots;
                    break;
                default:
                    break;
            }
        }
        //04-22
        addEquipPanel.gameObject.SetActive(false);
        // Yes 버튼에 클릭 이벤트 리스너 추가
        btn_YesEquipAdd.onClick.AddListener(OnYesButtonClick);

        // No 버튼에 클릭 이벤트 리스너 추가
        btn_NoEquipAdd.onClick.AddListener(OnNoButtonClick);

        if (GameMgr.single.LoadChecker() == true)
        {
            GameLoad();
            Debug.Log("Load Success");
            Debug.Log(GameMgr.playerData.GetPlayerName());
        }
    
        questDesc.text = questMgr.CheckQuest();

        SetPlayerDatas();

        SliderChange();

        //Tooltip
        canvaseWidth = canvas_Tooltip.GetComponent<CanvasScaler>().referenceResolution.x * 0.5f;
    }

    //03-31 Method Inventory - try.4
    public void SlotChange(int val)// slotChange 에서 slot의 slotNum을 차례대로 부여함
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;

            if (i < inventory.items.Count) // 인벤토리에 아이템이 있을 때만 버튼을 활성화
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inventory.SlotCnt += 5;
    }
    public void RedrawSlotUI()
    {
        for(int i = 0; i< slots.Length;i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i< inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].item.itemIndex = i;
            //Debug.Log(slots[i].item.itemIndex);

            slots[i].UpdateSloutUI();
        }
    }

    private void Update()
    {
        // Minimap 
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeRanderTextur();
            MinimapInteraction();
        }
        // Player Desc
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (DescCheck == true)
            {
                PlayerDesc.gameObject.SetActive(false);
                DescCheck = false;
            }
            else
            {
                PlayerDesc.gameObject.SetActive(true);
                DescCheck = true;
            }

        }
        
        // Sub Menu Set
        if (Input.GetButtonDown("Cancel") )
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
            }
            else
            {
                menuSet.SetActive(true);
            }
        }

        //Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveInventory();
        }
        //Tooltip
        MoveTooltip();

        /*Debug.Log("x:" + player.transform.position.x);시발시발시발
        Debug.Log("y:" + player.transform.position.y);*/
    }
    #region MinimapMethod
    private void ChangeRanderTextur()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        if (bigMinimapChek)
        {
            minimapRanderTexture.Release(); // 기존 텍스처 해제
            minimapRanderTexture.width = screenWidth; // 너비 변경
            minimapRanderTexture.height = screenHeight; // 높이 변경
            minimapRanderTexture.Create(); // 변경된 크기로 새로 생성
        }
        else
        {
            minimapRanderTexture.Release(); // 기존 텍스처 해제
            minimapRanderTexture.width = 1024; // 너비 변경
            minimapRanderTexture.height = 1024; // 높이 변경
            minimapRanderTexture.Create(); // 변경된 크기로 새로 생성
        }

    }
    private void MinimapInteraction()
    {
        if (bigMinimapChek)
        {
            smallMap.gameObject.SetActive(false);
            bigMap.gameObject.SetActive(bigMinimapChek);
            RawAlphaChange(rimgSmall, 0f);
            RawAlphaChange(rimgBig, 1f);


            Rect newRect = new Rect(0f, 0f, 1.2f, 1.2f);
            miniCamera.rect = newRect;

            bigMinimapChek = false;
        }
        else
        {
            smallMap.gameObject.SetActive(true);
            bigMap.gameObject.SetActive(bigMinimapChek);
            RawAlphaChange(rimgSmall, 1f);
            RawAlphaChange(rimgBig, 0f);

            Rect newRect = new Rect(0f, 0f, 2f, 2f);
            miniCamera.rect = newRect;

            bigMinimapChek = true;
        }
    }
    private void RawAlphaChange(RawImage rimg, float alpha)
    {
        Color color = rimg.color;
        color.a = alpha;
        rimg.color = color;
    }
    #endregion
    #region ToggleBtn
    public void ToggleSubButtons()
    {
        areSubButtonsVisible = !areSubButtonsVisible;

        if (areSubButtonsVisible)
        {
            ShowSubButtons();
        }
        else
        {
            HideSubButtons();
        }
    }
    private void ShowSubButtons()
    {
        foreach (GameObject subButton in subButtons)
        {
            subButton.gameObject.SetActive(true);
        }
    }
    private void HideSubButtons()
    {
        foreach (GameObject subButton in subButtons)
        {
            subButton.gameObject.SetActive(false);
        }
    }
    #endregion
    private void SetPlayerDatas()
    {
        tmp_PlayerName.text = GameMgr.playerData.NAME;
        playerGold = GameMgr.playerData.player_Gold;
        playerLevel = GameMgr.playerData.player_level;

        tmp_PlayerLevle.text = "Lv." + GameMgr.playerData.player_level .ToString();
        tmp_PlayerGold.text = GameMgr.playerData.player_Gold .ToString();

        this.player_Max_HP = GameMgr.playerData.max_Player_Hp;
        this.player_Cur_HP = GameMgr.playerData.cur_Player_Hp;

        this.player_Max_SN = GameMgr.playerData.max_Player_Sn;
        this.player_Cur_SN = GameMgr.playerData.cur_Player_Sn;

        this.player_Max_MP = GameMgr.playerData.max_Player_Mp;
        this.player_Cur_MP = GameMgr.playerData.cur_Player_Mp;

        this.player_Atk_Speed = GameMgr.playerData.atk_Speed;
        this.player_Atk_Range = GameMgr.playerData.atk_Range;
        this.player_Base_Atk_Dmg = GameMgr.playerData.base_atk_Dmg;
        this.player_Max_EXP = GameMgr.playerData.player_max_Exp;
        this.player_Cur_EXP = GameMgr.playerData.player_cur_Exp;
        

        s_HP.value = this.player_Cur_HP / this.player_Max_HP;
        s_SN.value = this.player_Cur_SN / this.player_Max_SN;
        s_EXP.value = this.player_Cur_EXP / this.player_Max_EXP;

    }
    public void SliderChange()
    {
        s_HP.value = this.player_Cur_HP / this.player_Max_HP;
        s_SN.value = this.player_Cur_SN / this.player_Max_SN;
        s_EXP.value = this.player_Cur_EXP / this.player_Max_EXP;
        tmp_PlayerGold.text = this.playerGold.ToString();

        if (playerGold <= 0)
        {
            tmp_PlayerGold.text = "0";
        }
    }

    public void TalkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();// Ray가 스캔했을때  LayerMask가 Obejct인 오브젝트가 부착중인 ObecjtData를  Ray가 오브젝트를 스캔 했을 때만 추출해서 TossTalkData메서드의 매개변수로 사용함.
        TossTalkData(objectData.id, objectData.isNpc);

        //Debug.Log(objectData.id.ToString());// 04-23 Debug

        imgTalkPnel.gameObject.SetActive(isActionTalk);// isActionTalk의 true/false 상태를 따라가기때문에 이렇게 작성해주면 코드 깔끔해짐 
    }

    private void TossTalkData(int scanObj_ID, bool scanObj_isNpc)
    {
        //Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";

        // isAnim
        if (typeTextEffect.isAnim)
        {
            typeTextEffect.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questMgr.GetQuestTalkIndex(scanObj_ID);
            talkData = talkMgr.GetTalk(scanObj_ID + questTalkIndex, talkIndex);
        }

        //End Talk
        if (talkData == null)
        {
            //Debug.Log("NulltalkData // ToosTalkData: " + scanObj_ID); // 04 -23 Debug
            /*if (AllEquipChek())
            {
                questMgr.CheckQuest(scanObj_ID);
                return;
            }*/

            isActionTalk = false;
            talkIndex = 0;
            questDesc.text = questMgr.CheckQuest(scanObj_ID);

            if (scanObj_ID == 8000)
            {
                SceneManager.LoadScene("Battle");//아니면여기에 던전에입장하시겠습니까? 예, 아니오, Wall, 값을 넣고 던져서 예누르면 wall로 텔포,아니오누르면 그냥 retrun하게하는식으로하면~ 야매 맵이동구현 뚝딲
                GameSave();
            }
            
            /*if (questMgr.questId ==10 && questMgr.questActionIndex == 0)
            {
                questMgr.receptionist[0].SetActive(false);
                questMgr.receptionist[1].SetActive(true);
            }*/

            return;
        }

        //Continue Talk
        if (scanObj_isNpc)
        {
            //Debug.Log("ContinueTalk // ToosTalkData: " + scanObj_ID);// 04-23 Debug
            typeTextEffect.SetMsg(talkData.Split(':')[0]);// .Split()  ':' 구분자 : 를 통하여 문자열을 배열로 나눠주는 함수

            //show Portrait
            imgPortrait.sprite = talkMgr.GetPortrait(scanObj_ID, int.Parse(talkData.Split(':')[1]));
            imgPortrait.color = new Color(1, 1, 1, 1);// npc가 맞으면 초상화이미지 활성화
        }
        else
        {
            //Debug.Log("else ContinueTalk // ToosTalkData: " + scanObj_ID); // 04 -23 Debug
            typeTextEffect.SetMsg(talkData);
            imgPortrait.color = new Color(1, 1, 1, 0);// npc가 아니면 초상화이미지 비활성화
        }

        isActionTalk = true;
        talkIndex++;
    }
    public void GameSave()
    {
        if (menuSet.activeSelf)
        {
            menuSet.SetActive(false);
        }

        SaveData gameSaveData = new SaveData(GameMgr.playerData.GetPlayerName(), playerLevel, playerGold, questMgr.questId, questMgr.questActionIndex, player_Max_HP, player_Cur_HP, player_Max_SN, player_Cur_SN, player_Max_MP, player_Cur_MP, player_Atk_Speed, player_Atk_Range, player_Base_Atk_Dmg, player_Max_EXP, player_Cur_EXP );
        SaveSystem.Save(gameSaveData, "save");

        //  Player DayCount, Player Inventory, Player Desc (Stat, Name, Job, Gold ... ect)
    }
    public void GameLoad()
    {
        SaveData loadData = SaveSystem.Load("save");

        //Load Player Data => save_001.x, save_001.y, save_001.questId, save_001.QuestActionIndex 

        GameMgr.single.OnSelectPlayer(loadData.playerName);

        /*Vector3 lodingPosition = new Vector3(loadData.playerX, loadData.playerY);
        player.transform.position = lodingPosition;*/
        //Debug.Log("load x, y: "+loadData.playerX +", "+ loadData.playerY);
        //SetNowPosition(loadData.playerX, loadData.playerY);

        GameMgr.playerData.max_Player_Hp = loadData.p_max_hp;
        GameMgr.playerData.cur_Player_Hp = loadData.p_cur_hp;

        GameMgr.playerData.max_Player_Sn = loadData.p_max_sn;
        GameMgr.playerData.cur_Player_Sn = loadData.p_cur_sn;

        GameMgr.playerData.max_Player_Mp = loadData.p_max_mp;
        GameMgr.playerData.cur_Player_Mp = loadData.p_cur_mp;

        GameMgr.playerData.player_max_Exp = loadData.p_max_Exp;
        GameMgr.playerData.player_cur_Exp = loadData.p_cur_Exp;


        GameMgr.playerData.player_Gold = loadData.p_gold;
        GameMgr.playerData.player_level = loadData.p_level;


        GameMgr.playerData.atk_Speed = loadData.p_atk_speed;
        GameMgr.playerData.atk_Range = loadData.p_atk_range;
        GameMgr.playerData.base_atk_Dmg = loadData.p_base_atk_Dmg;


        questMgr.questId = loadData.questId;
        questMgr.questActionIndex = loadData.questActionIndex;
        questMgr.ControlQuestObejct();
        //GetNowPositon();

    }
    #region PlayerPosition
    public void SetNowPosition(float x, float y)
    {
        //Debug.Log("set x, y: " + x + ", " + y);
        //lodingPosition.x = x;
        //lodingPosition.y = y;
    }
    public void GetNowPositon()
    {
        //Debug.Log("get x, y: " + lodingPosition.x + ", " + lodingPosition.y);
        //player.transform.position = lodingPosition;
        //Debug.Log("P x, y: " + player.transform.position.x + ", " + player.transform.position.y);
    }
    #endregion
    public void OnVideoOption_S1()
    {
        if (videoOption_S1.gameObject.activeSelf)
        {
            videoOption_S1.gameObject.SetActive(false);
        }
        else
        {
            videoOption_S1.gameObject.SetActive(true);
        }
    }
    public void OffVideoOption_S1()
    {
        videoOption_S1.gameObject.SetActive(false);
    }
    public void ActiveInventory()
    {
        activeInventory = !activeInventory;
        inventory_panel.SetActive(activeInventory);
        if (activeInventory == false)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                tooltip.SetActive(false);
            }
        }
    }
    public void SetupTooltip(string _name, string _title, string _desc, Sprite _img)
    {
        textName.text = _name;

        textTitle.text = _title;

        textDesc.text = _desc;

        imgIcon.sprite = _img;
    }
    private void MoveTooltip()
    {
        tooltip.transform.position = Input.mousePosition;
        // 04-15 ToolTip
        tooltipRect = tooltip.GetComponent<RectTransform>();

        if (tooltipRect.anchoredPosition.x + tooltipRect.sizeDelta.x > canvaseWidth)
            tooltipRect.pivot = new Vector2(1, 0);
        else
            tooltipRect.pivot = new Vector2(0, 0);
    }

    public void OnClickedQuite()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    /*public void GetMoseItem(Slot _slot)
    {
        this.nowSlot = _slot;
        this.dragIcon = _slot.itemIcon;
    }*/
    /*public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
        nowSlot = slots[dragIndex];// 슬롯 등록

        dragIcon.transform.position = eventData.position;
        dragIcon = nowSlot.itemIcon;

        dragIcon.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Now ing...");
        dragIcon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        dragIcon.gameObject.SetActive(false);
    }*/

    public void OnYesButtonClick()
    {
        equipmnet = true;
        Debug.Log("AddEquip Name: " + nowSlot.item.itemName);
        Debug.Log("AddEquip Type: " + nowSlot.item.itemType);

        WearEquipment();
        if (AllEquipChek())
        {
            questMgr.questActionIndex = 2;
        }
    }
    public void OnNoButtonClick()
    {
        equipmnet = false;
        addEquipPanel.gameObject.SetActive(false);
    }

    /*public void WearEquipMent()
    {
        for (int i = 0; i < targetSlots.Length; i++)
        {
            if (targetSlots[i].item.itemType == nowSlot.item.itemType)
            {
                Debug.Log("Sucess Equip Add: "+ nowSlot.item.itemName);
                
                targetSlots[i].slotnum = nowSlot.slotnum;
                targetSlots[i].item = nowSlot.item;
                targetSlots[i].itemIcon = nowSlot.itemIcon;
            }
            else
            {
                continue;
            }
        }
    }*/

    public void WearEquipment()
    {
        int index = 0;
        // 현재 선택된 슬롯의 아이템을 복제하여 대상 슬롯에 추가
        for (int i = 0; i < targetSlots.Length; i++)
        {
            if (targetSlots[i].item.itemType == nowSlot.item.itemType)
            {
                Debug.Log("Success Equip Add: " + nowSlot.item.itemName);
                index = nowSlot.slotnum;
                // 아이템 복제
                Item clonedItem = nowSlot.item;

                // 아이템 인덱스 설정
                clonedItem.itemIndex = nowSlot.slotnum;

                // 아이콘 설정
                targetSlots[i].itemIcon.sprite = nowSlot.itemIcon.sprite;
                targetSlots[i].itemIcon.gameObject.SetActive(true);
                targetSlots[i].eqipChek = true;
                // 아이템 설정
                targetSlots[i].item = clonedItem;
            }
        }
        // 사용한 아이템 제거 
        inventory.RemoveItem(slots[index].slotnum);
        RedrawSlotUI();

        nowSlot = null;

        addEquipPanel.gameObject.SetActive(false);
    }
    public bool AllEquipChek()
    {
        int sum = 0;
        for (int i = 0; i < targetSlots.Length; i++)
        {
            if (targetSlots[i].eqipChek == true)
            {
                sum++;
            }
        }

        if (sum == (targetSlots.Length))
        {
            Debug.Log("장비 장착 완료");
            Receptionist_1();
            return true;
        }
        return false;
    }

    public void TutorialDungeonClear()
    {
        Debug.Log("튜토리얼 던전 클리어");
        Receptionist_1();
    }
    private void Receptionist_1()
    {
        questMgr.receptionist[0].SetActive(false);
        questMgr.receptionist[1].SetActive(true);
    }


}
