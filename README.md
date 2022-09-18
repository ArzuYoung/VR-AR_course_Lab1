# Основы работы с Unity
Отчет по лабораторной работе #1 выполнила:
- Рзаева Арзу Масуд гызы
- РИ300012
Отметка о выполнении заданий:

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
Ознакомиться с основными функциями Unity и взаимодействием с объектами внутри редактора.

## Задание 1
### Пошагово выполнить каждый пункт раздела "ход работы" с описанием и примерами реализации задач
Ход работы:
- Создать новый проект из шаблона 3D – Core. Проверить, что настроена интеграция редактора Unity и Visual Studio Code. 
- Создать объекты Plane, Cube, Sphere. Установить компонент Sphere Collider для объекта Sphere. Настроить Sphere Collider в роли триггера. Объект куб перекрасить в красный цвет. Добавить кубу симуляцию физики, при это куб не должен проваливаться под
Plane.

![alt text](ScreenShots/pic1.1.PNG)

- Написать скрипт, который будет выводить в консоль сообщение о том, что объект Sphere столкнулся с объектом Cube. При столкновении Cube должен менять свой цвет на зелёный, а при завершении столкновения обратно на красный.

Представленный ниже скрипт доступен в [репозиитории](https://github.com/ArzuYoung/VR-AR_course_Lab1/blob/main/VR-AR_course_Lab1/Assets/CheckCollider.cs)

```c#

public class CheckCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Произошло столкновение с " + other.gameObject.name);
        other.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }
    
    private void OnTriggerExit(Collider other) {
        Debug.Log("Завершено столкновение с " + other.gameObject.name);
        other.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
}

```
Столкновение:

![alt text](ScreenShots/pic1.2.PNG)

Конец столкновения:

![alt text](ScreenShots/pic1.3.PNG)

### Бонус
В качестве отработки навыков был создан эффект "взрыва" сферы при столкновении.

![alt text](ScreenShots/boom.gif)

Представленный ниже скрипт доступен в [репозиитории](https://github.com/ArzuYoung/VR-AR_course_Lab1/blob/main/VR-AR_course_Lab1/Assets/DestroyObject.cs)

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5.0f;
    public float force = 10.0f;
    public GameObject prefabBoomPoint;
    public GameObject prefabBoomSphere;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Sphere")
        {
            Destroy(other.gameObject);
            Vector3 boomPosition = other.gameObject.transform.position;
            Instantiate(prefabBoomPoint, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Instantiate(prefabBoomSphere, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(boomPosition, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, boomPosition, radius, 3.0f);
                }
            }
        }
    }
}


```

## Задание 2
### Продемонстрируйте на сцене в Unity следующее:
#### Что произойдёт с координатами объекта, если он перестанет быть дочерним?

Будучи дочерним элемент разделяет поведение родительского элемента => их координаты связаны и при движении изменяются на одинаковые величины. На примере двух сфер: дочерний элемент также падает и разбивается

![alt text](ScreenShots/WithChildElement.gif)

Когда элемент перестает быть дочерним его координаты самостоятельны и ни от каких других элементов не зависят. В этом примере шар перестает двигаться вместе с другой сферой

![alt text](ScreenShots/NoChildElement.gif)

#### Создайте три различных примера работы компонента RigidBody
##### Рассмотрим Куб в разных состояниях
- Включено свойство Is Kinematic => объект сможет управляться только при помощи своей трансформации:
![alt text](ScreenShots/IsKinematic.gif)

- Включено свойство Use Gravity => объект подчиняется законам гравитации:
![alt text](ScreenShots/UseGravity.gif)

- Отключены свойства Is Kinematic и Use Gravity => объект не подчиняется законам гравитации, но его может "толкнуть" другой объект и он будет двигаться:
![alt text](ScreenShots/NoIsKinematicUseGravity.gif)

## Задание 3
### Реализуйте на сцене генерацию n кубиков. Число n вводится пользователем после старта сцены.
- В ходе выполнения задания было создано поле `inputField`, в которое пользователь вводит число, и после завершения редактирования изменяется количество кубов на экране на введенное пользователем.

Представленный ниже скрипт доступен в [репозиитории](https://github.com/ArzuYoung/VR-AR_course_Lab1/blob/main/VR-AR_course_Lab1/Assets/Task3Script.cs)

```c#

using UnityEngine;
using UnityEngine.UI;

public class Task3Script : MonoBehaviour
{
    public GameObject cube;
    public int count;
    
    void Start()
    {
        count = 0;
    }

    public void MakeObjects(string inputField){
        count = int.Parse(inputField);
        
        for (int i = 0; i < count; ++i){
            Instantiate(cube, new Vector3(-10 + i, 5, 0), cube.transform.rotation);
        }
    }

}

```

![alt text](ScreenShots/pic3.1.PNG)

![alt text](ScreenShots/pic3.2.PNG)

## Выводы

Таким образом, в ходе лабораторной работы я ознакомилась с интерфейсом и основными функциями Unity, научилась взаимодействовать с объектами внутри редактора и с помощью скриптов, а также создавать и удалять объекты.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

