using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerControlStickyGaze : MonoBehaviour {

    public float playerMoveSpeed = 4f;
    public Camera cam;
    public Collider testCollider;
    public Collider floor;
    public GameObject player;
    int timer;
    bool timeme;
    bool played = false;
    bool moved = false;
    public AudioSource testAudio1;
    public AudioSource testAudio2;
    public MeshRenderer testswap;
    public Text evil;
    public Text good;
    public Text taskDisplay;
    public Text requestorText;
    GameObject greenCube;
    public GameObject TEMPNEWOBJ;
    public ParticleSystem presentGet;
    public ParticleSystem presentGet2;
    public ParticleSystem presentGet3;
    public ParticleSystem presentGet4;

    //success combo audios
    public AudioClip combo1;
    public AudioClip combo2;
    public AudioClip combo3;
    public AudioClip combo4;
    public AudioClip combo5;
    public AudioClip combo6;

    //door transforms for playing sounds
    public Transform bedroomDoor, showroomDoor, theaterDoor;

    //working around the scale issues
    Vector3 tempScale;

    //new request feedback
    public GameObject newReq;
    int feedTimer;
    bool playingNewReq = false;

    //result objects
    public GameObject puzzle1_result;
    public GameObject puzzle2_result;
    public GameObject puzzle3_result;
    public GameObject puzzle4_result;
    public GameObject puzzle5_result;
    public GameObject puzzle6_result;

    //boolean
    bool errorMsg = false;
    public int errorTimer = 0;
    public Text errorLabel;
    public Image errorBG;

    //door icon
    public Animator doorAnim;
    public AudioSource doorSound;

    //audio
    public AudioClip comboSuccess;
    public AudioClip errorSound;
    public AudioSource accelerator;
    bool acc = false;

    List<GameObject> MyObjects = new List<GameObject>();
    myInfo objectInfo; //info on the object from MyObjects[0]
    myInfo secondObjectInfo;    //info on the second object in MyObjects

    public bool lookedAtSomethingElse = false;

    public ParticleSystem reticlePS;
    public Animator reticleAnim;
    public Image reader; 
    public Text WorldLabel;
    public bool inSallysRoom;
    public Text task;
    public string currentRequestor;

    public GameObject door1;//APT2
    public Vector3 door1OpenRotation;
    public Vector3 door1OpenPosition;
    public GameObject door2;//APT3
    public Vector3 door2OpenRotation;
    public Vector3 door2OpenPosition;
    public GameObject door3;//Theatre
    public Vector3 door3OpenRotation;
    public Vector3 door3OpenPosition;

    //Analytics Tools - ignore these, they are measuring data for us
    //Specifically I am measuring how much time it takes for players to reach the correct combinations
    public int puzzle1Timer = 0;
    //And how many false combination attempts there are between puzzles
    public int numWrongCombos = 0;

    List<myInfo> AllObjsWithInfo = new List<myInfo>();  //reference list of all the "MyInfo" scripts in the scene

    //Public Tasks
    public int taskNum = 1;

   

    // Use this for initialization
    void Start() {
        //Analytics
        //Tinylytics.AnalyticsManager.LogCustomMetric("New Session", "STARTED");

        currentRequestor = "Enbee";

        for (int i = 0; i < FindObjectsOfType<myInfo>().Length; i++) {  //populate the list of "MyInfo" scripts
            AllObjsWithInfo.Add(FindObjectsOfType<myInfo>()[i]);
        }
        //Debug.Log(AllObjsWithInfo[0].gameObject.name);

        presentGet.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + 2);
    }

    void Update() {
        transform.localScale = transform.localScale;
        //Debug.Log("Current requestor: " + currentRequestor);
        //Debug.Log("my obj" + MyObjects.Count);
        Cursor.lockState = CursorLockMode.Locked;
        
        ResetGame();    //Restart Scene on Escape Press/P press
        Movement();     //Move the player
        CastRay();      //Cast a ray from screen center into space
        LaunchStuff();  //push r to launch stuff in the direction of the camera
        Detach();       //push space to release stuff
        Combine();      //check combining objects

        //Analytics
        puzzle1Timer++;

        if(playingNewReq && feedTimer < 90)
        {
            //waiting until new request feedback has been seen before killing partle fx
            feedTimer++;
        }
        else
        {
            feedTimer = 0;
            playingNewReq = false;
            newReq.GetComponent<ParticleSystem>().Stop();
        }

        if (errorMsg)
        {
            Debug.Log("I SHOULD BE SHOWING");
            errorBG.enabled = true;
            errorLabel.enabled = true;
            errorLabel.text = "404: At least one combination adjective not found";
            errorTimer++;
            if (errorTimer > 60f)
            {
                errorTimer = 0;
                errorMsg = false;
                errorLabel.text = "";
                errorBG.enabled = false;
                Debug.Log("DONE");
            }
        }

        //Rest the eroor sound this is TEMP
        if(testAudio1.clip != errorSound && !testAudio1.isPlaying)
        {
            testAudio1.clip = errorSound;
        }
        

        if (presentGet.isEmitting) {
            StartCoroutine("stopParti");
            //presentGet.Stop();
        }
    }

    public void cleanCam() {
        //safely eject items from the camera load
        //deleting
        foreach (GameObject go in MyObjects) {
            if (gameObject.name != "Player") {
                Debug.Log("im deleting:" + go.name);
                Destroy(go);
            }
        }
        MyObjects.Clear();
        cam.transform.DetachChildren();
    }

    public void nextTask() {
        //sets up next task
        taskNum++;
        //skip Task 4
        if (taskNum ==2 || taskNum == 4)
        {
            taskNum++;
        }
        taskDisplay.text = getTaskText();
        //assign the requestor character:
        switch (taskNum)
        {
            case 1:
                {
                    currentRequestor = "Enbee";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
            case 2:
                {
                    currentRequestor = "Dora";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
            case 3:
                {
                    currentRequestor = "Teju";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
            case 4:
                {
                    currentRequestor = "Enbee";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
            case 5:
                {
                    currentRequestor = "Dora";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
            case 6:
                {
                    currentRequestor = "Teju";

                    requestorText.text = currentRequestor.ToString();
                    break;
                }
        }
        //play sound and pfx
        newReq.GetComponent<Animator>().SetTrigger("newReq");
        newReq.GetComponent<ParticleSystem>().Play();
        newReq.GetComponent<AudioSource>().Play();
        playingNewReq = true;

    }

    string getTaskText() {
        switch (taskNum)
        {
            case 1:
                {
                    return "REQUEST: Hello, OTTO, are you on? My honey, the executive, is coming over. Bring me something DIRTY to get me in the mood, but also CLEAN to keep it classy.";
                    break;
                }
            case 2:
                {
                    return "REQUEST: I need something provocative in my photography portfolio. Make me something SHOCKING and EVIL to photograph.";
                    break;
                }
            case 3:
                {
                    return "REQUEST:  Oh OTTO you’re so lucky that you don’t have to deal with the struggles of being a true artist. I need inspiration! Bring me something HOT to fuel my artistic flame and something... RISKY! I want to feel that rush.";
                    break;
                }
            case 4:
                {
                    
                    return "REQUEST: Umm OTTO my pH is off balance… any chance you could bring me something ACIDIC and BASIC to balance it out? I’d really appreciate it.";
                    break;
                }
            case 5:
                {
                    return "REQUEST: About to live tweet the fireworks show! Make me something TASTY and EXPLOSIVE to eat during the show and I'll unlock the theatre for you!";
              
                    break;
                }
            case 6:
                {
                    return "REQUEST: OTTO, I feel like I danced myself to death last night! I’m trying not to panic but there’s no way I can get down at the disco dying of disco fever! Please Doc Robot make me something MEDICINAL and FUNKY to help get me back on my platform wearin’ feet.";
                    break;
                }
        }
        return "";
    }

    void turnOffAllLabels() {
        WorldLabel.text = "";
        reader.enabled = false;
    }

    bool checkMatchingTags(string key1, string key2) {
        //compare tags and return if the game objects grabbed match those tags.
        bool got1 = false;
        bool got2 = false;

        foreach (GameObject g in MyObjects) {
            if (!got1 || !got2) {
                if (g.tag == key1) {
                    got1 = true;
                    Debug.Log("My tag is xxxxxx " + g.tag);

                }
                else {
                    Debug.Log("My tag is " + g.tag);
                }
            }
            if (!got2) {
                if (g.tag == key2) {
                    got2 = true;

                }
                else {
                    Debug.Log("My tag is " + g.tag);
                }
            }
        }

        if (got1 && got2) {
            return true;
        }
        else {
            return false;
        }

    }

    public void detachItems() {
        //remove items from sticky gaze
        Rigidbody[] items = cam.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody i in items) {
            // i.useGravity = true;
            i.useGravity = false;
            if (i.gameObject.GetComponent<myInfo>() != null) {
                i.gameObject.GetComponent<myInfo>().grabbed = false;
               // i.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                i.gameObject.GetComponent<Rigidbody>().useGravity = false;//was true
            }
           
        }
    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log("triggered");
        if (other.tag == "reset") {
            //Debug.Log("this worked");
            for (int i = 0; i < AllObjsWithInfo.Count; i++) {
                AllObjsWithInfo[i].gameObject.transform.position = AllObjsWithInfo[i].startPos;
                AllObjsWithInfo[i].gameObject.transform.rotation = AllObjsWithInfo[i].startRot;
            }
        }
    }

    void ResetGame() {
        //Restart Scene on Escape Press
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();

        }
        if (Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadScene(0);
        }
    }

    void Movement() {
        float z = Input.GetAxis("Vertical") * Time.deltaTime;
        gameObject.transform.position += z * transform.forward * playerMoveSpeed;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        gameObject.transform.position += x * transform.right * playerMoveSpeed;
 

        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.93f, gameObject.transform.position.z);

        player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 2f, player.transform.localEulerAngles.z);
        float tempY = Input.GetAxis("Mouse Y") * -2f;
        if(tempY < -160f)
        {
            //dont allow them to look any further up
            tempY = -160f;
        }
        if (tempY > 160f)
        {
            //dont allow them to look any further down
            tempY = 160f;
        }
        player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x + tempY, player.transform.localEulerAngles.y, player.transform.localEulerAngles.z);
        // player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * -2f, player.transform.localEulerAngles.y, player.transform.localEulerAngles.z);

        //cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 2f, cam.transform.localEulerAngles.z);
        //cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * -2f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
        //cancel all forces acting on the player if they are not pressing a button
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //accelerator.Stop();

            AudioManager.instance.StopPlayerMovementSound();

            acc = false;
        }
        else
        {
            if (!acc)
            {
                //accelerator.Play();
                AudioManager.instance.StartPlayerMovementSound(transform, gameObject.GetComponent<Rigidbody>());
                acc = true;
            }

        }

        foreach(GameObject go in MyObjects)
        {
            if (go.transform.position.y < cam.transform.position.y - 0.5f)
            {
                //snap the object back up if it falls
                Vector3 tempPos = go.transform.position;
                tempPos.y = cam.transform.position.y - 0.5f;
                go.transform.position = tempPos;
            }
        }

    }

    void CastRay() {
        Ray ray = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);  //show the debug ray
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f)) {    //the 10f is the length the ray extends in distance 
            //A collision occured between the ray and a thing
            if (hit.collider != null && hit.collider != floor && hit.collider.gameObject != cam && Input.GetKeyDown(KeyCode.Mouse0)) {
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                hit.collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                hit.collider.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //pick it up
                if (hit.collider.gameObject.tag == "person")
                {
                    //hahaha
                }
                else
                {
                    //PICK UP OBJECT
                    
                    // Debug.Log(tempScale.x);
                    hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    
                    //Debug.Log("HOLDING: parti turned off");
                    hit.collider.gameObject.GetComponent<myInfo>().binaryParti = false;    //turn off partis when you're holding the object
                    hit.collider.transform.parent = cam.transform;//was cam.transform
                    MyObjects.Add(hit.collider.gameObject);

                    //newAudio
                    AudioManager.instance.PlaySound(AudioManager.instance.pickupObjectSound, gameObject);

                    if (hit.collider.GetComponent<myInfo>() != null)
                    {
                        hit.collider.GetComponent<myInfo>().grabbed = true;
                        hit.collider.gameObject.GetComponent<myInfo>().binaryParti = false;
                    }
                    //hit.collider.transform.GetComponent<Rigidbody>().velocity = cam.transform.GetComponent<Rigidbody>().velocity;
                }
            }
            else if (hit.collider != null && hit.collider != floor && hit.collider.gameObject != cam) {
                //display the label
                //showLabel();
               
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    //click to release
                    if (hit.collider.tag == "combined") {
                        //this is a combined object, break it it
                    }
                }
                if (hit.collider.gameObject.GetComponent<myInfo>() != null) {
                    hit.collider.gameObject.GetComponent<myInfo>().watched = true;
                    if (hit.collider.gameObject.GetComponent<myInfo>().label != null) {
                        WorldLabel.enabled = true;
                        //play the little animation
                        if (!lookedAtSomethingElse)
                        {
                            Debug.Log("IM SEEING A THING");
                            reticleAnim.SetTrigger("looking");
                            reticlePS.Play();
                            reader.enabled = true;
                            lookedAtSomethingElse = true;

                            //newAudio
                            AudioManager.instance.PlaySound(AudioManager.instance.hoverOverObjectSound, gameObject);
                        }

                        if (hit.collider.gameObject.GetComponent<myInfo>().wrongCombine == false) {
                            WorldLabel.text = hit.collider.gameObject.GetComponent<myInfo>().label;
                        }
                        else {
                            WorldLabel.text = hit.collider.gameObject.GetComponent<myInfo>().tagReveal;
                        }

                        //put particles here

                        //WorldLabel.text = hit.collider.gameObject.GetComponent<myInfo>().label;


                        if (!hit.collider.gameObject.GetComponent<myInfo>().grabbed) {
                            //Debug.Log("turning on parti");
                            hit.collider.gameObject.GetComponent<myInfo>().binaryParti = true;
                        }
                        else {
                            hit.collider.gameObject.GetComponent<myInfo>().binaryParti = false;
                        }
                    }
                }
            }
        }
        else {
            //didnt catch anything on ray
            lookedAtSomethingElse = false;
            turnOffAllLabels();
            for (int i = 0; i < MyObjects.Count; i++) {
                Debug.Log("NOT LOOKING: turning off labels...");
                MyObjects[i].GetComponent<myInfo>().binaryParti = false;
            }
        }
    }

    void LaunchStuff() {
       /* if (Input.GetKey(KeyCode.R)) {
            Transform[] grabbed = cam.GetComponentsInChildren<Transform>();
            cam.transform.DetachChildren();
            detachItems();
            foreach (Transform t in grabbed) {
                if (t.GetComponent<Rigidbody>() != null) {
                    t.GetComponent<Rigidbody>().gameObject.GetComponent<myInfo>().grabbed = false;
                    t.GetComponent<Rigidbody>().AddRelativeForce(cam.transform.forward * 300f);
                    t.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }*/
    }

    void Detach() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            //this.transform.parent = null;
            detachItems();
            cam.transform.DetachChildren();
            //Remove from the MyObjects list
            MyObjects.Clear();
            //Debug.Log("YOU DOOD IT");

            //newAudio
            AudioManager.instance.PlaySound(AudioManager.instance.putdownObjectSound, gameObject);

        }
    }

    void Combine() {
        //this is to combine objects with tags
        if (Input.GetKeyDown(KeyCode.Space)) {

            objectInfo = MyObjects[0].GetComponent<myInfo>();    //the label of the object we're referring to a lot here on out
            secondObjectInfo = MyObjects[1].GetComponent<myInfo>();     //the second thing you picked up

            switch (taskNum) {
                case 1: {
                        //find clean and dirty
                        if (checkMatchingTags("clean", "dirty") && MyObjects.Count <= 2) {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 1 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 1", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;//standardize this to be a uniform location infront of camera
                            GameObject temp = Instantiate(puzzle1_result, transform.position + (transform.forward * 1.5f), transform.rotation);//move this to infront of camera
                            temp.GetComponent<myInfo>().label = "Tidies";
                            temp.name = "SlimyCucumber";
                            temp.GetComponent<myInfo>().sallyObject = true;

                            //sound
                            //testAudio1.clip = comboSuccess;
                            //testAudio1.Play();
                            //testAudio2.clip = combo1;
                            //testAudio2.Play();
                            
                            //newAudio
                            AudioManager.instance.PlaySound(AudioManager.instance.createUndies, gameObject);

                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                           // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                           // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            //temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;
                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);

                            //Debug.Log(MyObjects.Count);
                        }
                        
                        else {
                            ComboFailed();
                        }

                        break;
                    }
                case 2: {
                        //find shocking and evil
                        ///////////////////////////////////
                        //NOT USED IN GAME AUDIO VERSION///
                        ///////////////////////////////////

                        if (checkMatchingTags("shocking", "evil") && MyObjects.Count <= 2)
                        {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 2 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 2", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;
                            GameObject temp = Instantiate(puzzle2_result, transform.position + (transform.forward * 1.5f), transform.rotation);//move this to infront of camera
                            temp.GetComponent<Rigidbody>().freezeRotation = false;
                            temp.transform.Rotate(-90, 0, 0);
                            temp.GetComponent<myInfo>().label = "Death by Neon";
                            temp.name = "GlowingScythe";
                            temp.GetComponent<myInfo>().sallyObject = true;

                            //sound
                            testAudio1.clip = comboSuccess;
                            testAudio1.Play();
                            testAudio2.clip = combo2;
                            testAudio2.Play();

                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                            // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                            // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            // temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;

                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);
                        }
                       
                        else
                        {
                            ComboFailed();
                        }

                        break;
                      
                    }
                case 3: {
                        //find comedic and dramatic
                        if (checkMatchingTags("hot", "risky") && MyObjects.Count <= 2) {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 3 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 3", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;
                            GameObject temp = Instantiate(puzzle3_result, transform.position + (transform.forward * 1.5f), transform.rotation);//move this to infront of camera
                            temp.GetComponent<Rigidbody>().freezeRotation = false;
                            temp.transform.Rotate(-90, 0, 0);
                            temp.GetComponent<myInfo>().label = "Inevitable Spicy Poops";
                            temp.name = "SpicyPoops";
                            temp.GetComponent<myInfo>().sallyObject = true;

                            //sound
                            //testAudio1.clip = comboSuccess;
                            //testAudio1.Play();
                            //testAudio2.clip = combo3;
                            //testAudio2.Play();

                            //newAudio
                            AudioManager.instance.PlaySound(AudioManager.instance.createSpicy, gameObject);


                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                            // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                            // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            // temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;

                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);
                        }
                       
                        else {
                            ComboFailed();
                        }

                        break;
                    }
                case 4:
                    {
                        //find comedic and dramatic
                        ////////////////////////////////////
                        ///NOT USED IN GAME AUDIO VERSION///
                        ////////////////////////////////////
                        
                        if (checkMatchingTags("basic", "acidic") && MyObjects.Count <= 2)
                        {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 4 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 4", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;
                            GameObject temp = Instantiate(puzzle4_result, transform.position + (transform.forward * 1.5f), transform.rotation);//move this to infront of camera
                            temp.GetComponent<myInfo>().label = "Margarita";
                            temp.name = "Margarita";
                            temp.GetComponent<myInfo>().sallyObject = true;

                            //sound
                            testAudio1.clip = comboSuccess;
                            testAudio1.Play();
                            testAudio2.clip = combo4;
                            testAudio2.Play();

                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                            // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                            // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            // temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;

                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);
                        }
                       
                        else
                        {
                            ComboFailed();
                        }

                        break;
                    }
                case 5:
                    {
                        
                        //find tasty and explosive
                        if (checkMatchingTags("tasty", "explosive") && MyObjects.Count <= 2)
                        {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 5 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 5", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;
                            GameObject temp = Instantiate(puzzle5_result, transform.position + (transform.forward * 1.5f), transform.rotation);    //move this to infront of camera

                            //sound
                            //testAudio1.clip = comboSuccess;
                            //testAudio1.Play();
                            //testAudio2.clip = combo5;
                            //testAudio2.Play();

                            //newAudio
                            AudioManager.instance.PlaySound(AudioManager.instance.createPopcorn, gameObject);

                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                            // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                            // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            temp.GetComponent<myInfo>().label = "Popcorn";
                            temp.name = "Popcorn";
                            temp.GetComponent<myInfo>().sallyObject = false;

                            //open the door


                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            //temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;

                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);
                        }
                        
                        else
                        {
                            ComboFailed();
                        }

                        break;
                       
                    }
                case 6:
                    {
                        //find comedic and dramatic
                        if (checkMatchingTags("funky", "medicinal") && MyObjects.Count <= 2)
                        {
                            //success
                            Debug.Log("YOU COMBINED CORRECTLY");
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Puzzle 6 Solve Time (sec)", (puzzle1Timer / 60).ToString());
                            //Tinylytics.AnalyticsManager.LogCustomMetric("Wrong Combination Attempts To Puzzle 6", numWrongCombos.ToString());
                            numWrongCombos = 0;
                            puzzle1Timer = 0;
                            //Remove old objects for new one
                            Vector3 pos = MyObjects[0].transform.position;
                            GameObject temp = Instantiate(puzzle6_result, transform.position + (transform.forward * 1.5f), transform.rotation);//move this to infront of camera
                            temp.GetComponent<myInfo>().label = "Boogie RX";
                            temp.name = "DiscoPills";
                            temp.GetComponent<myInfo>().sallyObject = true;

                            //sound
                            //testAudio1.clip = comboSuccess;
                            //testAudio1.Play();
                            //testAudio2.clip = combo6;
                            //testAudio2.Play();

                            //newAudio
                            AudioManager.instance.PlaySound(AudioManager.instance.createMedicine, gameObject);


                            //particles
                            presentGet.transform.position = temp.transform.position;
                            //presentGet.transform.parent = this.gameObject.transform;
                            presentGet.Play();
                            presentGet2.transform.position = temp.transform.position;
                            // presentGet2.transform.parent = this.gameObject.transform;
                            presentGet2.Play();
                            presentGet3.transform.position = temp.transform.position;
                            //presentGet3.transform.parent = this.gameObject.transform;
                            presentGet3.Play();
                            presentGet4.transform.position = temp.transform.position;
                            // presentGet4.transform.parent = this.gameObject.transform;
                            presentGet4.Play();
                            temp.GetComponent<myInfo>().partiStart = true;

                            detachItems();
                            cleanCam();
                            temp.GetComponent<Rigidbody>().useGravity = false;
                            // temp.GetComponent<Rigidbody>().isKinematic = false;
                            temp.GetComponent<Rigidbody>().freezeRotation = true;
                            temp.GetComponent<Rigidbody>().angularDrag = 0f;
                            temp.GetComponent<Rigidbody>().mass = 1f;

                            //Show the item's label on the present's tag
                            Debug.Log(temp.transform.GetChild(0).name);
                            Debug.Log(temp.transform.GetChild(0).transform.GetChild(0).name);
                            TextMeshProUGUI tmpro = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();//sorry this is because it defaults the text like three children down :(
                            tmpro.SetText(temp.name);
                        }
                        
                        else
                        {
                            ComboFailed();
                        }

                        break;
                    }
            }
        }
    }

    IEnumerator stopParti() {
        yield return new WaitForSeconds(1f);
        presentGet.Stop();
        presentGet2.Stop();
        presentGet3.Stop();
        presentGet4.Stop();
    }

    void ComboFailed()
    {

        errorMsg = true;
        //play the sound
        //errorBG.GetComponent<AudioSource>().Play();

        //newAudio
        AudioManager.instance.PlaySound(AudioManager.instance.comboFailed, gameObject);

        Debug.Log("COMBO DIDN'T WORK");
        numWrongCombos++;

        objectInfo.wrongCombine = true;
        secondObjectInfo.wrongCombine = true;

    }

}
