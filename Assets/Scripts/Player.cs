using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;

    [SerializeField] private GameObject _vaccinePrefab;

    [SerializeField] private float _vaccinationRate = 0.3f;

    [SerializeField] private int _lives = 3;

    [SerializeField] private SpawnManager _spawnManager;

    private float _timeToVaccinate = -1f;
    
    // STUFF ABOUT COLOURS
    private float _colorChannel = 1f;
    private MaterialPropertyBlock _mpb;

    // Start is called before the first frame update
    void Start()
    {
        // STUFF ABOUT COLOURS
        if (_mpb == null)
        {
            _mpb = new MaterialPropertyBlock();
            _mpb.Clear();
            this.GetComponent<Renderer>().GetPropertyBlock(_mpb);
        }
        // reset player pos on start
        transform.position = new Vector3(x: 0f, y: 0f, z: 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Vaccinate();
    }
    
    // public cause Rona needs access
    public void Damage()
    {
         // reduce _lives by one if hit
         _lives -= 1;
         
         // STUFF ABOUT COLOURS
         _colorChannel -= 0.5f;
         _mpb.SetColor("_Color", new Color(_colorChannel, g: 0, b: _colorChannel, a: 1f));
         this.GetComponent<Renderer>().SetPropertyBlock(_mpb);

         this.GetComponent<Renderer>().material.color =
             new Color(r: _colorChannel, g: 0, b: _colorChannel, a: 1f);
         
         // if health is 0 destroy player
        if (_lives == 0)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    void PlayerMovement()
    {
        // read player input for x and y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // apply player movement
        Vector3 playerTranslate = new Vector3(
            x: 1f * horizontalInput * _speed * Time.deltaTime, 
            y: 1f * verticalInput * _speed * Time.deltaTime,
            z: 0f);
        transform.Translate(playerTranslate);
        
        // Player Boundaries
        if (transform.position.y > 2f)
        {
            // force y-pos to 0
            transform.position = new Vector3(transform.position.x,
                y: 2f,
                z: 0f);
        }
        else if (transform.position.y < -4.1f)
        {
            transform.position = new Vector3(transform.position.x,
                y: -4.1f,
                z: 0f);
        }
        // horizontal go-around
        if (transform.position.x > 11.4f)
        {
            transform.position = new Vector3(
                x: -11.4f,
                transform.position.y,
                z: 0f);
        }
        else if (transform.position.x < -11.4f)
        {
            transform.position = new Vector3(
                x: 11.4f,
                transform.position.y,
                z: 0f);
        }
    }

    void Vaccinate()
    {
        // if certain button (spacebar) is pressed
        // then instantiate vaccine prefab
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _timeToVaccinate))
        {
            // add the rate to the current time to set
            // the future possible vaccination time
            _timeToVaccinate = Time.time + _vaccinationRate;
            Instantiate(_vaccinePrefab, transform.position
                                        + new Vector3(x: 0f, y: 0.78f, z: 0),
                Quaternion.identity);
        }
    }
}
