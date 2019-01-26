using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteSheet
{
    public string name;
    public List<Sprite> frames;
    public int framesPerSecond;
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [Header("Player Settings")]
    public bool autoPlay = true;
    public string defaultAnimation;
    public List<SpriteSheet> spriteSheets = new List<SpriteSheet>();

    [Header("Advanced (Optional)")]
    [Tooltip("Animation will be applied to given Game Object. MUST have SpriteRenderer")]
    public GameObject customTarget;

    private bool isPlaying;
    private SpriteSheet currentSpriteSheet;
    private int currentFrame = 0;
    private float timeUntilNextFrame = 0;
    private SpriteRenderer spriteRenderer;

    public void Play()
    {
        this.isPlaying = true;
    }

    public void Pause()
    {
        this.isPlaying = false;
    }

    public void Stop()
    {
        this.Pause();
        this.DisplayFrame(0);
    }

    public void SetSpriteSheet(string name)
    {
        if (name != this.currentSpriteSheet.name) {
            this.currentSpriteSheet = this.GetSpritesheetByName(name);
            this.DisplayFrame(0);
        }
    }

    private SpriteSheet GetSpritesheetByName(string name)
    {
        return this.spriteSheets.Find(spriteSheet => spriteSheet.name == name);
    }

    private void DisplayFrame(int i)
    {
        this.spriteRenderer.sprite = this.currentSpriteSheet.frames[i];
        this.timeUntilNextFrame = 1f / this.currentSpriteSheet.framesPerSecond;
        this.currentFrame = i;
    }
    
    void Start()
    {
        if (this.customTarget != null) {
            this.spriteRenderer = this.customTarget.GetComponent<SpriteRenderer>();
        } else {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        this.isPlaying = this.autoPlay;
        this.SetSpriteSheet(this.defaultAnimation);
        this.DisplayFrame(0);
    }

    
    void Update()
    {
        if (this.isPlaying) {
            this.timeUntilNextFrame -= Time.deltaTime;
            if (this.timeUntilNextFrame <= 0) {
                this.currentFrame = (this.currentFrame + 1) % this.currentSpriteSheet.frames.Count;
                this.DisplayFrame(this.currentFrame);
            }
        }
    }
}
