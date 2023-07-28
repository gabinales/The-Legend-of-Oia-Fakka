using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public enum ArmaType
{
    Nenhuma,
    Grassblade
}

public class FMODEvents : MonoBehaviour
{
    [field: Header("Svard SFX")]
    [field: SerializeField] public EventReference svardAttack {get; private set; }
    [field: SerializeField] public EventReference danoHit {get; private set; }
    [field: Header("Alerta Contextual")]
    [field: SerializeField] public EventReference surpresaAlertaContextual {get; private set; }
    [field: Header("Inimigos")]
    [field: SerializeField] public EventReference plinioToggle {get; private set; }
    [field: Header("Ambiente")]
    [field: SerializeField] public EventReference waterFlowing {get; private set; }
    [field: SerializeField] public EventReference cemiterio {get; private set; }
    [field: SerializeField] public EventReference penhascoBeiramar {get; private set; }
    [field: Header("Interagíveis")]
    [field: SerializeField] public EventReference lootChestOpened {get; private set; }
    [field: SerializeField] public EventReference vasoSanitarioDescarga {get; private set; }
    [field: SerializeField] public EventReference vasoSanitarioDescargaEntupida {get; private set; }
    [field: Header("Switches")]
    [field: SerializeField] public EventReference alavancaToggle {get; private set; }
    [field: SerializeField] public EventReference cristalVerdeAtiva {get; private set; }
    [field: SerializeField] public EventReference cristalVerdeDesativa {get; private set; }
    [field: Header("Portas")]
    [field: SerializeField] public EventReference portaSimplesAbre {get; private set; }
    [field: SerializeField] public EventReference portaSimplesFecha {get; private set; }
    [field: Header("Destrutíveis")]
    [field: SerializeField] public EventReference gramaCortavel {get; private set; }
    [field: SerializeField] public EventReference cruzSimples {get; private set; }
    [field: Header("UI")]
    [field: SerializeField] public EventReference telaPauseAbre {get; private set; }
    [field: SerializeField] public EventReference telaPauseFecha {get; private set; }
    [field: SerializeField] public EventReference selecionaBotao {get; private set; }
    [field: SerializeField] public EventReference apertaBotao {get; private set; }
    [field: SerializeField] public EventReference abrePainelItem {get; private set; }
    [field: Header("Diálogo")]
    [field: SerializeField] public EventReference proximaFala {get; private set; }
    [field: SerializeField] public EventReference ultimaFala {get; private set; }
    [field: SerializeField] public EventReference confirmaFala {get; private set; }
    [field: SerializeField] public EventReference cancelaFala {get; private set; }
    [field: Header("Cutscenes")]
    [field: SerializeField] public EventReference cutscene2 {get; private set; }
    [field: SerializeField] public EventReference cutscene3 {get; private set; }
    [field: Header("Game")]
    [field: SerializeField] public EventReference novoItem {get; private set; }
    [field: SerializeField] public EventReference ativaSpawner {get; private set; }
    [field: SerializeField] public EventReference lootCollected {get; private set; }
    [field: SerializeField] public EventReference puzzleResolvido {get; private set; }
    [field: SerializeField] public EventReference caiuNoBuraco {get; private set; }
    [field: SerializeField] public EventReference usouTeleporte {get; private set; }

    private const string SvardAttackEventPath = "event:/Player/SvardAttack";
    private EventInstance svardAttackEventInstance;
    private PARAMETER_ID armaParameterId;
    private int currentArmaType = 0;

    private const string FootstepEventPath = "event:/Player/Footsteps";
    private EventInstance footstepEventInstance;
    private PARAMETER_ID terrainParameterId;
    private int currentTerrainType;

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Encontrou mais de uma instância de FMODEvents na Scene.");
        }
        instance = this;

        svardAttackEventInstance = RuntimeManager.CreateInstance(svardAttack);
        svardAttackEventInstance.getDescription(out EventDescription svardAttackEventDescription);
        svardAttackEventDescription.getParameterDescriptionByName("Arma", out PARAMETER_DESCRIPTION armaParameterDescription);
        armaParameterId = armaParameterDescription.id;

        footstepEventInstance = RuntimeManager.CreateInstance(FootstepEventPath);
        footstepEventInstance.getDescription(out EventDescription footstepEventDescription);
        footstepEventDescription.getParameterDescriptionByName("Terreno", out PARAMETER_DESCRIPTION terrainParameterDescription);
        terrainParameterId = terrainParameterDescription.id;
    }

    private void OnDestroy()
    {
        svardAttackEventInstance.release();
        footstepEventInstance.release();
    }

    // ------------------------------------------------------------- SFX do ataque 
    public void SetArmaType(ArmaType armaType)
    {
        int newArmaType = (int)armaType;
        if (currentArmaType != newArmaType)
        {
            currentArmaType = newArmaType;
            svardAttackEventInstance.setParameterByID(armaParameterId, currentArmaType);
        }
    }

    public void PlaySvardAttackSound()
    {
        svardAttackEventInstance.start();
    }

    // ------------------------------------------------------------- SFX de passos
    public void SetFootstepTerrain(int terrainType)
    {
        if (currentTerrainType != terrainType)
        {
            currentTerrainType = terrainType;
            footstepEventInstance.setParameterByID(terrainParameterId, terrainType);
        }
    }

    public void PlayFootstepSound()
    {
        footstepEventInstance.start();
    }
}