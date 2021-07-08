using TMPro;
using UnityEngine;

public class Display : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// Text used to display the FPS.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textFps;
    
    /// <summary>
    /// Text used to display the highest encountered FPS value.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textFpsHigh;
    
    /// <summary>
    /// Text used to display the lowest encountered FPS value.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textFpsLow;
    
    /// <summary>
    /// Text used to display how long each frame took to render in milliseconds (ms).
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textMs;
    
    /// <summary>
    /// Text used to display the highest encountered ms value. 
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textMsHigh;
    
    /// <summary>
    /// Text used to display the lowest encountered ms value.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _textMsLow;

    /// <summary>
    /// The amount of time that should elapse between data calculations.
    /// </summary>
    [SerializeField, Range(0.0f, 2f)]
    private float _sampleDuration = 1f;
    
    /// <summary>
    /// The number of frames that have been captured.
    /// </summary>
    private int _frames;
    
    /// <summary>
    /// Total time elapsed.
    /// </summary>
    private float _totalDuration;
    
    /// <summary>
    /// The time between frames.
    /// </summary>
    private float _singleFrameDuration;
    
    /// <summary>
    /// Used to record the highest FPS and ms value encountered between samples.
    /// </summary>
    private float _highFpsDuration = float.MaxValue;

    /// <summary>
    /// Used to record the lowest FPS and ms values encountered between samples.
    /// </summary>
    private float _lowFpsDuration;
    
    /// <summary>
    /// Used to record the highest FPS value encountered over total play time.
    /// </summary>
    private float _highestFps = float.MaxValue;
    
    /// <summary>
    /// Used to record the lowest FPS value encountered over total play time.
    /// </summary>
    private float _lowestFps;

    #endregion
    
    #region --Unity Specific Methods--

    private void Update()
    {
        UpdateSampleData();
        UpdateHighestDuration();
        UpdateLowestDuration();
    }
    
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Tracks frames and total duration to update the display data every _sampleDuration.
    /// </summary>
    private void UpdateSampleData()
    {
        _singleFrameDuration = Time.unscaledDeltaTime;
        _frames += 1;
        _totalDuration += _singleFrameDuration;
        
        if (_totalDuration >= _sampleDuration)
        {
            _textFps.SetText("{0:0}", _frames / _totalDuration);
            _textMs.SetText("{0:1}", 1000f * _totalDuration / _frames);
            _frames = 0;
            _totalDuration = 0f;
            _highFpsDuration = float.MaxValue;
            _lowFpsDuration = 0f;
        }
    }

    /// <summary>
    /// Updates the highest FPS and ms values.
    /// </summary>
    private void UpdateHighestDuration()
    {
        if (_singleFrameDuration < _highFpsDuration) 
        {
            _highFpsDuration = _singleFrameDuration;
            if (_highFpsDuration < _highestFps)
            {
                _highestFps = _highFpsDuration;
                _textFpsHigh.SetText("{0:0}", 1f / _highestFps);
                _textMsLow.SetText("{0:1}", 1000f * _highestFps);
            }
        }
    }

    /// <summary>
    /// Updates the lowest FPS and ms values.
    /// </summary>
    private void UpdateLowestDuration()
    {
        if (_singleFrameDuration > _lowFpsDuration && Time.unscaledTime > 2.0f)
        {
            _lowFpsDuration = _singleFrameDuration;
            if (_lowFpsDuration > _lowestFps)
            {
                _lowestFps = _lowFpsDuration;
                _textFpsLow.SetText("{0:0}", 1f / _lowestFps);
                _textMsHigh.SetText("{0:1}", 1000f * _lowestFps);
            }
        }
    }
    
    #endregion
    
}
