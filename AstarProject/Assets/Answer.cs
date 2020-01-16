using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    
    private class Employee    :  IComparer, IStructuralComparable
    {
        public string Name { get; set; }
        public int Salary { get; set; }

        public int Compare(object x, object y)
        {
            var xObj = x as Employee;
            var yObj = y as Employee;

            
            if (xObj.Salary < yObj.Salary) return -1;
            else if (xObj.Salary > yObj.Salary) return 1;
            else return 0;
        }


       public  int CompareTo(object other, IComparer comparer)
        {
            return comparer.Compare(this, other);

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("sadfsadfsadfsadfsadf");
        answer9();
    }

    // Update is called once per frame
    void Update()
    {
         //answer9();
    }
    void answer1()
    {
        /*
         인터페이스 : 그 클래스가 가져야될 형식을 지정해줍니다. 상속 받는 것처럼 보이지만 상속이 아닌 형태로서 여러개를 동시에 받아서 사용할 수 있습니다
                        인터페이스로 받은 형식은 반드시 클래스 안에서 내용을 만들어줘야됩니다
         추상 클래스 : 인터페이스와 동일한 역할을 하지만 상속을 받는 것이라 다중상속이 지원되지 않습니다. 동시에 여러개를 받아야 될 경우, 사용할 수 없는 방법입니다.
         */
    }
    void answer2()
    {
        /*
         game object : 유니티에서 상속 구조에서 최상위 클래스. 
         mono bahaviour : 유니티가 돌아가는 구조를 잡아주는 클래스
         component :게임오브젝트에 붙일 수 있는 컴포넌트 구조형식. 게임 오브젝트라면 컴포넌트를 붙이는 방법으로 붙일 수 있습니다.
         */
    }
    
    void answer3()
    {
        /*
         const : 변수로 선언시 한번 지정한 값을 바꿀 수 없게 상수화 시킵니다. 
         입력인자에 선언을 해줄 경우 그 값을 바꿀 수 없습니다. 
         처음 지정한 값은 cast를 활용하는 경우를 제외하고 바꿀 수 없습니다. 
         readonly : const를 이용하여 property를 보호할 수 있습니다.
                    awake에서 딱 한번 값을 바꿀 수 있습니다. 그 이후로는  const와 마찬가지로 바꿀 수 없습니다
         */
    }
    void answer4()
    {
        /*
         삭제 방법 :  에디터에서 삭제하기, 컴포넌트의 멤버 함수로 destory를 호출하는 함수를 만들어 destroy(this)를 이용하면 그 컴포넌트르 ㄹ지울 수 있습니다
         */
    }
    void answer5()
    {
        /*
         [serialize filed] 변수 이름, 변수 이름 {get;set;}, const 변수형 변수이름, readonly 변수형  변수 이름
         */
    }
   private void answer6()
    {
        List<int> input = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

       
     }
   private void answer7()
    {
        /**/
        List<int> input = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        

       // input.RemoveAll( );

        //input.RemoveAll(
        //    [this](input)->List<int>{
        //    List<int> result = new List<int>();
        //    foreach (var i in input)
        //    {
        //        if (i % 2 == 0) result.Add(i);
        //    }
        //    return result;
        //});
    }
   private void answer8()
    {
        /**/
    }
 private   void answer9()
    {
       // print("SDFSADFSADFSADFSADFSADF");
        List<Employee> employees = new List<Employee>();
        //int j = 0;
        for(int i=0;i<10;i++)
        {
            employees.Add(new Employee());
        }
       for(int i=0;i<10;i++)
        {
            //employees[i] = new Employee();
            employees[i].Name = "No" + i;
            employees[i].Salary = (int)Random.Range(0.0f, 600.0f);
        }

        employees.Sort(employees[0].Compare);
        foreach(var i in employees)
        {
            Debug.Log(i.Name.ToString() + " 's salary : " + i.Salary.ToString());
        }


    }

}
