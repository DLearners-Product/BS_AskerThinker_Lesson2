using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Globalization;
using FrostweepGames.Plugins.Core;
using UnityEngine.SceneManagement;
using TMPro;


namespace FrostweepGames.Plugins.GoogleCloud.TextToSpeech
{
    public class ImmersiveReader : MonoBehaviour
    {

        public static ImmersiveReader OBJ_ImmersiveReader;
        GCTextToSpeech _gcTextToSpeech;
        Voice _currentVoice;
        Voice[] _voices;
        CultureInfo _provider;
        string content;
        AudioSource audioSource;
        public GameObject[] ImmersiveObjects;
        public Image BG;
        public int VoiceNumber;
        public double VoicePitch, VoiceSpeed;
        public GameObject G_ruler, G_focus;
        public string dictWord;
       

        void Start()
        {
            OBJ_ImmersiveReader = this;
            _gcTextToSpeech = GCTextToSpeech.Instance;
            audioSource = _gcTextToSpeech.gameObject.GetComponent<AudioSource>();
            _provider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            _provider.NumberFormat.NumberDecimalSeparator = ".";
            _gcTextToSpeech.GetVoicesSuccessEvent += _gcTextToSpeech_GetVoicesSuccessEvent;
            _gcTextToSpeech.SynthesizeSuccessEvent += _gcTextToSpeech_SynthesizeSuccessEvent;
            _gcTextToSpeech.SynthesizeFailedEvent += _gcTextToSpeech_SynthesizeFailedEvent;

        }
        private void Update()
        {
            if (G_ruler.activeInHierarchy)
            {
                Vector3 pos = Input.mousePosition;
                pos.z = G_ruler.transform.position.z - Camera.main.transform.position.z;
                G_ruler.transform.position = Camera.main.ScreenToWorldPoint(pos);
            }

            if (G_focus.activeInHierarchy)
            {
                Vector3 pos = Input.mousePosition;
                pos.z = G_focus.transform.position.z - Camera.main.transform.position.z;
                G_focus.transform.position = Camera.main.ScreenToWorldPoint(pos);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (G_ruler.activeInHierarchy)
                    G_ruler.SetActive(false);

                if (G_focus.activeInHierarchy)
                    G_focus.SetActive(false);
            }

        }

         public void SynthesizeButtonOnClickHandler()
        {

            GetVoicesButtonOnClickHandler();
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Text>() != null)
            {
                content = EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text;
                Debug.Log("TEXT : " + content);
            }
            if (EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>() != null)
            {
                content = EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>().text;
                Debug.Log("TMP ORIGINAL: " + content);
                while (content.Contains("<"))
                {
                   int startIndex = content.IndexOf("<");
                   int stringLength = content.Length - 1;
                   string unwantedString = content.Substring(startIndex, stringLength - startIndex);
                   int endIndex = unwantedString.IndexOf(">") + 1;
                   unwantedString = content.Substring(startIndex, endIndex);
                   content = content.Replace(unwantedString.Trim(), "");
                   Debug.Log("TMP REDEFINED : "+content);
                }
            }

            if (string.IsNullOrEmpty(content) || _currentVoice == null)
                return;

            _gcTextToSpeech.Synthesize(content, new VoiceConfig()
            {
                gender = _currentVoice.ssmlGender,
                languageCode = _currentVoice.languageCodes[0],
                name = _currentVoice.name
            }, false, VoicePitch, VoiceSpeed, _currentVoice.naturalSampleRateHertz);
        }


        void _gcTextToSpeech_SynthesizeSuccessEvent(PostSynthesizeResponse response, long requestId)
        {
            audioSource.clip = _gcTextToSpeech.GetAudioClipFromBase64(response.audioContent, Constants.DEFAULT_AUDIO_ENCODING);
            audioSource.Play();
        }


        void _gcTextToSpeech_SynthesizeFailedEvent(string error, long requestId)
        {
            Debug.Log(error);
        }

        void _gcTextToSpeech_GetVoicesSuccessEvent(GetVoicesResponse response, long requestId)
        {
            _voices = response.voices;
            _currentVoice = _voices[VoiceNumber];
        }
        void GetVoicesButtonOnClickHandler()
        {
            _gcTextToSpeech.GetVoices(new GetVoicesRequest()
            {
                languageCode = _gcTextToSpeech.PrepareLanguage((Enumerators.LanguageCode)11)
            });
        }

      
        public void BUT_openPanel(GameObject panel)
        {
            panel.SetActive(true);
        }
        public void BUT_colorPick(int x)
        {
            if (x == 1)
            {
                for (int i = 0; i < ImmersiveObjects.Length; i++)
                {
                    if(ImmersiveObjects[i]!=null)
                    {
                    if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                        ImmersiveObjects[i].GetComponent<TextMeshProUGUI>().color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
                }}
            }
            if (x == 2)
            {
                BG.color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
            }
            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        }
        public void BUT_fontPick()
        {
            for (int i = 0; i < ImmersiveObjects.Length; i++)
            {
                 if(ImmersiveObjects[i]!=null)
                 {
                if (ImmersiveObjects[i].GetComponent<TextMeshProUGUI>() != null)
                        ImmersiveObjects[i].GetComponent<TextMeshProUGUI>().font = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().font;
                }
            }
            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        }
        public void BUT_zoomPick(float scale)
        {
            BG.gameObject.transform.localScale = new Vector2(scale,scale);
            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}