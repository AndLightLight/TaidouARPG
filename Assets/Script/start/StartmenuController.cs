using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

public class StartmenuController : MonoBehaviour {

    public static StartmenuController _instance;

    public TweenScale startpanelTween;
    public TweenScale loginpanelTween;
    public TweenScale registerpanelTween;
    public TweenScale serverpanelTween;
    public TweenPosition startpanelTweenPos;
    public TweenPosition characterselectTween;
    public TweenPosition charactershowTween;

    public UIInput usernameInputLogin;
    public UIInput passwordInputLogin;

    public UILabel usernameLabelStart;
    public UILabel servernameLabelStart;

    public static string username;
    public static string password;
    public static ServerProperty sp;
    public static List<Role> roleList = null;


    public UIInput usernameInputRegister;
    public UIInput passwordInputRegister;
    public UIInput repasswordInputRegister;

    public UIGrid serverlistGrid;

    public GameObject serveritemRed;
    public GameObject serveritemGreen;

    private bool haveInitServerlist = false;

    public GameObject serverSelectedGo;

    public GameObject[] characterArray;
    public GameObject[] characterSelectedArray;

    private GameObject characterSelected;//当前选择的角色

    public UIInput characternameInput;
    public Transform characterSelectedParent;

    public UILabel nameLabelCharacterselect;
    public UILabel levelLabelCharacterselect;

    private LoginController loginController;
    private RegisterController registerController;
    private RoleController roleController;

    void Awake() {
        _instance = this;
        loginController = this.GetComponent<LoginController>();
        registerController = this.GetComponent<RegisterController>();
        roleController = this.GetComponent<RoleController>();

        roleController.OnAddRole += OnAddRole;
        roleController.OnGetRole += OnGetRole;
        roleController.OnSelectRole += OnSelectRole;
    }

    void Start() {
        //InitServerlist();
    }

    void OnDestroy()
    {
        if (roleController != null)
        {
            roleController.OnAddRole -= OnAddRole;
            roleController.OnGetRole -= OnGetRole;
        }
    }

    public void OnGetRole(List<Role> roleList)
    {
        StartmenuController.roleList = roleList;
        //这个是得到了角色信息之后的处理
        if (roleList != null && roleList.Count > 0)
        {
            //进入角色显示的界面
            Role role = roleList[0];
            ShowRole(role);
        }
        else
        {
            //进入角色创建的界面
            ShowRoleAddPanel();
        }
    }

    public void OnAddRole(Role role)
    {
        if (roleList == null)
        {
            roleList = new List<Role>();
        }
        roleList.Add(role);
        ShowRole(role);
    }

    public void OnSelectRole()
    {
        characterselectTween.gameObject.SetActive(false);
        AsyncOperation operation = Application.LoadLevelAsync(1);
        LoadSceneProgressBar._instance.Show(operation);
    }

    public void ShowRole( Role role )
    {
        PhotonEngine.Instance.role = role;
        ShowCharacterselect();

        nameLabelCharacterselect.text = role.Name;
        levelLabelCharacterselect.text = "Lv." + role.Level;

        int index = -1;
        for (int i = 0; i < characterArray.Length; i++) {
            if ((characterArray[i].name.IndexOf("boy") >= 0 && role.IsMan) || (characterArray[i].name.IndexOf("girl") >= 0 && role.IsMan==false)) {
                index = i;
                break;
            }
        }
        if (index == -1) {
            return;
        }
        GameObject.Destroy(characterSelectedParent.GetComponentInChildren<Animation>().gameObject);// 销毁现有的角色
        //创建新选择的角色
        GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = characterSelectedParent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(1, 1, 1);
    }


    public void OnGamePlay()
    {
        roleController.SelectRole(PhotonEngine.Instance.role);
    }

    public void OnUsernameClick() {
        //输入帐号进行登录 
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();
    }
    public void OnServerClick() {
        //选择服务器
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));

        serverpanelTween.gameObject.SetActive(true);
        serverpanelTween.PlayForward();

        //InitServerlist();//初始化服务器列表
    }
    public void OnEnterGameClick() {
        
        loginController.Login(username,password);

        //2,进入角色选择界面
        //startpanelTweenPos.PlayForward();
        //HidePanel(startpanelTweenPos.gameObject);
        //characterselectTween.gameObject.SetActive(true);
        //characterselectTween.PlayForward();
    }

    public void ShowCharacterselect()
    {
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }

    public void HideStartPanel()
    {
        startpanelTweenPos.PlayForward();
        StartCoroutine(HidePanel(startpanelTweenPos.gameObject));
    }

    //隐藏面板
    IEnumerator HidePanel(GameObject go) {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    public void OnLoginClick() {
        //得到用户名和密码 存储起来
        username = usernameInputLogin.value;
        password = passwordInputLogin.value;
        //返回开始界面
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel( loginpanelTween.gameObject ));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        usernameLabelStart.text = username;
    }

    public void OnRegisterShowClick() {
        //隐藏当前面板，显示注册面板
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        registerpanelTween.gameObject.SetActive(true);
        registerpanelTween.PlayForward();
    }

    public void OnLoginCloseClick() {
        //返回开始界面
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void OnCancelClick() {
        //隐藏注册面板
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
        //显示登录面板
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();
    }
    public void OnRegisterCloseClick() {
        OnCancelClick();
    }
    public void OnRegisterAndLoginClick() {
        username = usernameInputRegister.value;
        password = passwordInputRegister.value;
        string rePasswrod = repasswordInputRegister.value;


        if (username == null || username.Length <= 3)
        {
            MessageManager._instance.ShowMessage("用户名不能少于三个字符");
            return;
        }
        if (password == null || password.Length <= 3)
        {
            MessageManager._instance.ShowMessage("密码不能少于三个字符");
            return;
        }

        if (password != rePasswrod)
        {
            MessageManager._instance.ShowMessage("密码输入不一致",2);
            return;
        }

        registerController.Register(username,password,this);

        //usernameLabelStart.text = username;
    }

    public void HideRegisterPanel()
    {
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
    }

    public void ShowStartPanel()
    {
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void InitServerlist() {
        if (haveInitServerlist) return;

        //1，连接服务器 取得游戏服务器列表信息
        //TODO
        //2，根据上面的信息 添加服务器列表

        for (int i = 0; i < 20; i++) {
    //            public string ip="127.0.0.1:9080";
    //             public string name="1区 马达加斯加";
    //public int count=100;
            string ip = "127.0.0.1:9080";
            string name = (i + 1) + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject go = null;
            if (count > 50) {
                //火爆
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemRed);
            } else {
                //流畅
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemGreen);
            }
            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;

            serverlistGrid.AddChild(  go.transform );
        }

        haveInitServerlist = true;
    }

    public void OnServerselect(GameObject serverGo) {
        sp = serverGo.GetComponent<ServerProperty>();
        serverSelectedGo.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().text = sp.name;
    }

    public void OnServerpanelClose(){
        //隐藏服务器列表
        serverpanelTween.PlayReverse();
        StartCoroutine( HidePanel( serverpanelTween.gameObject ) );
        //显示开始界面
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        servernameLabelStart.text = sp.name;
    }

    public void OnCharacterClick( GameObject go ) {
        if (go == characterSelected) {
            return;
        }
        iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        if (characterSelected != null) {
            iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
        }
        characterSelected = go;

        //判断当前选择的角色是否已经创建，通过名字来判断
        foreach (var role in roleList)
        {
            if ((role.IsMan && go.name.IndexOf("boy") >= 0) || (role.IsMan == false && go.name.IndexOf("girl") >= 0))
            {
                characternameInput.value = role.Name;
            }
        }
    }
    //当点击了更换角色按钮
    public void OnButtonChangecharacterClick() {
        //隐藏自身的面板
        characterselectTween.PlayReverse();
        HidePanel(characterselectTween.gameObject);
        //显示展示角色的面板
        charactershowTween.gameObject.SetActive(true);
        charactershowTween.PlayForward();
    }

    public void ShowRoleAddPanel() {
        charactershowTween.gameObject.SetActive(true);
        charactershowTween.PlayForward();
    }

    public void OnCharactershowButtonSureClick() {

        //判断角色的名字是否符合规则
        if (characternameInput.value.Length <3)
        {
            MessageManager._instance.ShowMessage("角色的名字不能少于3个字符");
            return;
        }

        //判断当前的角色是否已经创建
        Role role = null;
        foreach (var roleTemp in roleList) {
            if ((roleTemp.IsMan && characterSelected.name.IndexOf("boy") >= 0) || (roleTemp.IsMan == false && characterSelected.name.IndexOf("girl") >= 0)) {
                characternameInput.value = roleTemp.Name;
                role = roleTemp;
            }
        }

        if (role == null)
        {
            Role roleAdd = new Role();
            roleAdd.IsMan = characterSelected.name.IndexOf("boy") >= 0 ? true : false;
            roleAdd.Name = characternameInput.value;
            roleAdd.Level = 1;
            roleAdd.Exp = 0;
            roleAdd.Coin = 20000;
            roleAdd.Diamond = 1000;
            roleAdd.Energy = 100;
            roleAdd.Toughen = 50;
            roleController.AddRole(roleAdd);
        }
        else
        {
            ShowRole(role);
        }

        OnCharactershowButtonBackClick();
    }
    public void OnCharactershowButtonBackClick() {
        charactershowTween.PlayReverse();
        HidePanel(charactershowTween.gameObject);

        characterSelected.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }

}