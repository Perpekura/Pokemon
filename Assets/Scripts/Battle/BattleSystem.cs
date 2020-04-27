using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public enum BattleState { START,PAYERTURN,ENEMYTURN,WIN,LOST }
public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefb;
    public GameObject enemyPrefb;   
    public BattleState state;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text dialogueText;
    
    public Text playerCurrent;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Animator animInfoPlayer;
    public Animator animPlayer;
    public Animator animSelectButton;

    GameObject BattleSelectPanel;   //总战斗面板
    GameObject SkillSelectPanel;    //技能面板 


    void Start()
    {
        BattleSelectPanel = GameObject.Find("右侧");
        SkillSelectPanel = GameObject.Find("技能选择");
        SkillSelectPanel.SetActive(false);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
       


    }
    private void Update()
    {
        PlayerAnimation();
        SelectMenuAnimation();
    }
    //玩家精灵以及状态栏动画切换
    void PlayerAnimation() {
        if (state == BattleState.PAYERTURN)
        {
            animInfoPlayer.SetBool("isPlayerBattle", true);
            animPlayer.SetBool("isPlayerBattle", true);
        }
        else
        {
            animInfoPlayer.SetBool("isPlayerBattle", false);
            animPlayer.SetBool("isPlayerBattle", false);
        }
    }

    //加载战斗
    IEnumerator SetupBattle() {
        GameObject playerGo = Instantiate(playerPrefb, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefb, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "啊！野生的 " + enemyUnit.unitName + " 钻出来了!";

        yield return new WaitForSeconds(2f);

        dialogueText.text = "冲啊！ " + enemyUnit.unitName + " ！";
        //初始化UI
        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        //玩家的回合（可以根据速度判断谁的回合，暂无）
        state = BattleState.PAYERTURN;

        

        PlayerTurn();
    }

    //玩家攻击方法
    IEnumerator PlayerAttack() {
        //攻击敌人
        bool isDead =enemyUnit.TakeDamage(playerUnit.damage);

        //更新敌人状态栏
        enemyHUD.setHP(enemyUnit.currentHP);


        dialogueText.text = playerUnit.unitName+"使用撞击!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //战斗结束
            state = BattleState.WIN;
            
            EndBattle();
        }
        else

        {
            //敌方回合
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

           
        
    }
    //玩家回合
    void PlayerTurn()
    {
        //打开战斗菜单
        BattleSelectPanel.SetActive(true);
        dialogueText.text =  playerUnit.unitName + " 想要做什么 ?";
    }

    //敌方回合
    IEnumerator EnemyTurn() {
        dialogueText.text = "敌方回合";

        yield return new WaitForSeconds(1f);

        bool isDead=playerUnit.TakeDamage(enemyUnit.damage);
        dialogueText.text =enemyUnit.unitName +"使用撞击!";
        yield return new WaitForSeconds(1f);
        playerHUD.setHP(playerUnit.currentHP);
        playerCurrent.text = playerUnit.currentHP.ToString();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PAYERTURN;
            PlayerTurn();
        }
    }
    //游戏结束的方法
    void EndBattle() {
        if (state == BattleState.WIN)
        {
            dialogueText.text =enemyUnit.unitName+"不行了!";
        }
        else
        {
            dialogueText.text =playerUnit.unitName+ "不行了!";
        }
    //战斗结束的方法
    }


    //键盘监听切换动画
    void SelectMenuAnimation() {
        if (EventSystem.current.currentSelectedGameObject == GameObject.Find("fight"))
        {
            animSelectButton.SetBool("isFight", true);
        }
        else
        {
            animSelectButton.SetBool("isFight", false);
        }
        if (EventSystem.current.currentSelectedGameObject == GameObject.Find("items"))
        {
            animSelectButton.SetBool("isItems", true);
        }
        else
        {
            animSelectButton.SetBool("isItems", false);
        }
        if (EventSystem.current.currentSelectedGameObject == GameObject.Find("pokemon"))
        {
            animSelectButton.SetBool("isPokemon", true);
        }
        else
        {
            animSelectButton.SetBool("isPokemon", false);
        }
        if (EventSystem.current.currentSelectedGameObject == GameObject.Find("run"))
        {
            animSelectButton.SetBool("isRun", true);
        }
        else
        {
            animSelectButton.SetBool("isRun", false);
        }
    }

    //按下攻击按钮
    public void OnAttackButton()
    {
        if (state != BattleState.PAYERTURN) {
            return;
        }
        SkillSelectPanel.SetActive(true);
        BattleSelectPanel.SetActive(false);

        //关闭战斗选项菜单

    }
    public void OnSkillSelectPanel() {
        StartCoroutine(PlayerAttack());
        SkillSelectPanel.SetActive(false);
    }
    //按下逃跑按钮
    public void OnRunButton()
    {
        
        SceneManager.LoadScene(1);

    }
}
