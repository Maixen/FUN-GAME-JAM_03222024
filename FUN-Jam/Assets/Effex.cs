using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effex : MonoBehaviour
{
    [SerializeField]
    private float swaySmooth;
    [SerializeField]
    private float swayMultiplier;

    [SerializeField]
    private float walkSmooth;
    [SerializeField]
    private float walkMultiplier;

    [SerializeField]
    private bool useShoot;

    [SerializeField]
    private Vector3 shootRecoil;
    [SerializeField]
    private float shootIntensityPos;
    [SerializeField]
    private float shootIntensityRot;
    [SerializeField]
    private float shootReturnSpeed;
    [SerializeField]
    private float shootSnapiness;
    [SerializeField]
    private float shootStaedinessReturnSpeed;
    [SerializeField]
    private float shootStaediness;

    private Quaternion weaponSwayRot;

    private Quaternion weaponWalkRot;

    private Vector3 weaponShootCurrentPos;
    private Vector3 weaponShootTargetPos;
    private Quaternion weaponShootCurrentRotation;
    private Quaternion weaponShootTargetRotation;

    private Vector3 endPos;
    private Quaternion endRot;

    public void Update()
    {
        WeaponSway();
        WeaponWalk();

        if (useShoot)
        {
            WeaponShootResolve();
        }

        endPos = weaponShootCurrentPos;

        if (useShoot)
            endRot = weaponSwayRot * weaponShootCurrentRotation * weaponWalkRot;
        else
            endRot = weaponSwayRot * weaponWalkRot;

        transform.localPosition = endPos;
        transform.localRotation = endRot;
    }

    public void OnFire()
    {
        if (useShoot)
            WeaponShoot();
    }

    private void WeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        weaponSwayRot = Quaternion.Slerp(weaponSwayRot, targetRotation, swaySmooth * Time.deltaTime);
    }

    private void WeaponWalk()
    {
        Quaternion rotationX = Quaternion.AngleAxis(Input.GetAxisRaw("Vertical") * walkMultiplier, Vector3.right);
        Quaternion rotationZ = Quaternion.AngleAxis(-Input.GetAxisRaw("Horizontal") * walkMultiplier, Vector3.forward);

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            Quaternion targetRotation = rotationX * rotationZ;
            weaponWalkRot = Quaternion.Slerp(weaponWalkRot, targetRotation, walkSmooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            weaponWalkRot = Quaternion.Slerp(weaponWalkRot, targetRotation, walkSmooth * Time.deltaTime);
        }
    }

    private void WeaponShoot()
    {
        float intensity = shootRecoil.x * shootIntensityPos;

        weaponShootTargetPos += new Vector3(0f, 0f, -intensity);

        Vector3 randomRotation = shootRecoil;

        weaponShootTargetRotation = Quaternion.Euler(weaponShootTargetRotation.x - randomRotation.x * shootIntensityRot, weaponShootTargetRotation.y + Random.Range(-randomRotation.y, randomRotation.y) * shootIntensityRot, weaponShootTargetRotation.z + Random.Range(-randomRotation.z, randomRotation.z) * shootIntensityRot);
    }

    private void WeaponShootResolve()
    {
        weaponShootTargetPos = Vector3.Lerp(weaponShootTargetPos, Vector3.zero, shootReturnSpeed * Time.deltaTime);

        weaponShootCurrentPos = Vector3.Slerp(weaponShootCurrentPos, weaponShootTargetPos, shootSnapiness * Time.fixedDeltaTime);

        weaponShootTargetRotation = Quaternion.Lerp(weaponShootTargetRotation, Quaternion.identity, shootStaedinessReturnSpeed * Time.deltaTime);

        weaponShootCurrentRotation = Quaternion.Slerp(weaponShootCurrentRotation, weaponShootTargetRotation, shootStaediness * Time.fixedDeltaTime);
    }
}
