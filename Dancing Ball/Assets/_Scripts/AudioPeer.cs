using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {
	public static AudioSource _audioSource;
	public static float[] _samples = new float[512];
	public static float[] _freqBand = new float[8];
	public static float[] _bandBuffer = new float[8];
	public static float _TypeOfSong = 0;
	float[] _bufferDecrease = new float[8];

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();

		if (SwitchScene.filePath.Length != 0) {
			LoadSong ();
		}
	}

	public void LoadSong(){
		StartCoroutine (LoadSongCoroutine());
	}


	IEnumerator LoadSongCoroutine(){
		//get the selected file 
		WWW www = new WWW ("file://" + SwitchScene.filePath);
		yield return www;
		// load it to AudioSource 
		_audioSource.clip = www.GetAudioClip (false,true);
		// play it 
		_audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		MakeFrequencyBands ();
		BandBuffer ();
		TypeOfSong ();
	}

	int MaxOfArray(float[] array){
		int MaxIndex = 0;
		for (int i = 1; i < 8; i++) {
			if (array [i] > array [MaxIndex]) {
				MaxIndex = i;
			}
		}
		return MaxIndex;
	}
	void TypeOfSong(){
		int MaxValueIndex = MaxOfArray (_bandBuffer);
		if (MaxValueIndex == 1 || MaxValueIndex == 2 || MaxValueIndex == 4 || MaxValueIndex == 6 ) {
			if (_TypeOfSong == 0)
				_TypeOfSong = 1;
			else
				_TypeOfSong = 0;
		} else if (MaxValueIndex == 0 || MaxValueIndex == 3 || MaxValueIndex ==  5 || MaxValueIndex == 7) {
			if (_TypeOfSong == 2)
				_TypeOfSong = 3;
			else
				_TypeOfSong = 2;
		}
	}

	void GetSpectrumAudioSource(){
		_audioSource.GetSpectrumData (_samples, 0, FFTWindow.Blackman);
	}

	void BandBuffer(){
		for (int g = 0; g < 8; g++) {
			if (_freqBand [g] > _bandBuffer [g]) {
				_bandBuffer [g] = _freqBand [g];
				_bufferDecrease [g] = 0.005f;
			}
			if (_freqBand [g] < _bandBuffer [g]) {
				_bandBuffer[g] -= _bufferDecrease [g];
				_bufferDecrease [g] *= 1.2f;
			}
		}
	}

	void MakeFrequencyBands(){
		/*
		 * 22050 / 512 = 43hertz sample
		 * 20 - 250 hertz
		 * 60 - 250 hertz
		 * 250 - 500 hertz
		 * 500 - 2000 hertz
		 * 2000 - 4000 hertz
		 * 4000 - 6000 hertz
		 * 6000 - 20000 hertz
		 * 
		 * 0 - 2 = 86 hertz
		 * 1 - 4 = 172 hertz - 87-258
		 * 2 - 8 = 344 hertz - 259-602
		 * 3 - 16 = 688 hertz - 603-1690
		 * 4 - 32 = 1376 hertz - 1291-10922
		 * 5 - 64 = 2752 hertz - 2667-5418
		 * 6 - 128 = 5504 hertz - 5419-10922
		 * 7 - 256 = 11008 hertz - 10923-21930
		 * 
		 */
		int count = 0;
		for (int i = 0; i < 8; i++) {
			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;

			if (i == 7) {
				sampleCount += 2;
			}
			for (int j = 0; j < sampleCount; j++) {
				average += _samples [count] * (count + 1);
				count++;
			}
			average /= count;
			_freqBand [i] = average * 10;
		}
	}
}
