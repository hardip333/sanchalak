app.controller('PaperTLMCtrl', function ($scope, $http, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Paper Definition";

    /* Varible Declaration */
    var newTLMlist = new Array();
    var newAMlist = new Array();
    $scope.ObjPaperTLMAM = {};
    $scope.ObjPaperTLMAM1 = {};
    var PaperTLMAM = [];
    $scope.PaperTLMAM = new Array();
    $scope.MstPaperTeachingLearningMap = {};
    $scope.MstPaperTeachingLearningMap1 = {};
    $scope.TLM_AM_MapList = {};
    var PaperIdLocal = "";
    $scope.showSecond = false;
    $scope.showThird = false;
    $scope.countcheck = {};

    /*Check LocalStorage for Data*/
    if ($localStorage.define) {
        $scope.MstPaperTeachingLearningMap = {};
        $scope.MstPaperTeachingLearningMap.PaperId = $localStorage.define.PaperId;
        $scope.MstPaperTeachingLearningMap.PaperName = $localStorage.define.PaperName;
        $scope.MstPaperTeachingLearningMap.PaperCode = $localStorage.define.PaperCode;
        $scope.MstPaperTeachingLearningMap.NoOfCredits1 = $localStorage.define.Credits;
        $scope.MstPaperTeachingLearningMap.MaxMarks = $localStorage.define.MaxMarks;         
        PaperIdLocal = $localStorage.define.PaperId;
    }

    /*Teaching Learning Method Get */
    $scope.MstTeachingLearningMethodGet = function () {

        $http({
            method: 'POST',
            url: 'api/MstPaperTeachingLearningMap/MstTLMAMPaperMapbyPaperId',
            data: $scope.MstPaperTeachingLearningMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.TLMList = response.obj;
                
                if ($scope.TLMList != undefined || $scope.TLMList != null) {
                    for (key in Object.keys($scope.TLMList)) {
                        if ($scope.TLMList[key].TLMCheckedStatus == true) {
                            newTLMlist.push($scope.TLMList[key]);
                        }
                    }
                    $scope.newTLMlist = newTLMlist;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Paper List Get*/
    $scope.MstPaperGet = function () {

        $http({
            method: 'POST',
            url: 'api/MstPaperTeachingLearningMap/MstPaperGet',
            data: $scope.MstPaperTeachingLearningMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.PaperList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /* Method to generate list of Teaching Learning Selected Method */
    $scope.CheckChangeTLM = function (TLM, checked) {
        
        if (checked) {
            newTLMlist.push(TLM);
        }
        else {            
            var TLMIndex = newTLMlist.map(function (item) { return item.Id; }).indexOf(TLM.Id);            
            newTLMlist.splice(TLMIndex, 1);
        }
        $scope.newTLMlist = newTLMlist;
    };

    /* Assessment Method List on the basis of Teaching Learning Method */
    $scope.MstAssessmentByTLM = function () {     
        
        $scope.MstPaperTeachingLearningMap.TeachingLearningMethodList = $scope.newTLMlist;
       
        $http({
            method: 'POST',
            url: 'api/MstPaperTeachingLearningMap/MstAssessmentMethodGetByTLMAMMap',
            data: $scope.MstPaperTeachingLearningMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.showSecond = true;
                $scope.TLM_AM_MapList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });

    };    

    /* Method to generate list of Assessment Method Selected Method */
    $scope.CheckChangeAM = function (AM, checked, TLMId) {

        if (checked) {
            newAMlist.push(AM.Id);
            var data = {};
            data.AssessmentMethodId = AM.Id;
            data.AssessmentMethodName = AM.AssessmentMethodName;
            data.TeachingLearningMethodId = TLMId.TeachingLearningMethodId;
            data.TeachingLearningMethodName = TLMId.TeachingLearningMethodName;
            data.PaperId = $scope.MstPaperTeachingLearningMap.PaperId;
            PaperTLMAM.push(data);

        }
        else {            
            var AMIndex = newAMlist.map(function (item) { return item.Id; }).indexOf(AM.Id);            
            newAMlist.splice(AMIndex, 1);
            var PaperTLMIndex = -4;
            if ($scope.PaperTLMAM != undefined || $scope.PaperTLMAM != null) {
                for (key in Object.keys($scope.PaperTLMAM)) {

                    if ($scope.PaperTLMAM[key].TeachingLearningMethodId == TLMId &&
                        $scope.PaperTLMAM[key].AssessmentMethodId == AM.Id) {
                        PaperTLMIndex = key;
                    }
                }
                PaperTLMAM.splice(PaperTLMIndex, 1); // It will remove the particular Index value from the List
            }

        }
        for (var i in PaperTLMAM) {                   
             
            var obj = new Array();
            var objUA = {};
            var objIA = {};
            objUA["CheckBox"] = false;
            objIA["CheckBox"] = false;
            objUA["Flag"] = true;
            objIA["Flag"] = true;
            objUA["Max"] = 0;
            objUA["Min"] = 0;
            objUA["ATName"] = "UA";
            objIA["ATName"] = "IA";
            objIA["Max"] = 0;
            objIA["Min"] = 0;
            obj.push(objUA);
            obj.push(objIA);
            PaperTLMAM[i].AssessType = obj;

            PaperTLMAM[i].NoOfCredits= 0;
            PaperTLMAM[i].AssessmentMethodMarks= 0;

        }
       
        $scope.newAMlist = newAMlist;
        $scope.PaperTLMAM = PaperTLMAM;
       
    };

    /* To View Third Table */
    $scope.AddPaperTLMAMMap = function () {

        $scope.showThird = true;
        $scope.ViewPaperTLMAMMap(PaperIdLocal);
        
    };

    /* To get Data from TeachingLearningPaperMap table */
    $scope.ViewPaperTLMAMMap = function (paperid1) {

        $scope.MstPaperTeachingLearningMap.PaperId = paperid1;
        $scope.TempPaperTLMAM = {};
        
        $http({
            method: 'POST',
            url: 'api/MstPaperTeachingLearningMap/MstPaperTeachingLearningMapGetByPaperId',
            data: $scope.MstPaperTeachingLearningMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.TempPaperTLMAM = response.obj;

                for (i in $scope.TempPaperTLMAM) {                
                    $scope.PaperTLMAM.push($scope.TempPaperTLMAM[i]); 
                }
            
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.checkValidation = function () {
        let count = 0;
        let count1 = 0;
        let total = 0;
        let flag = false;
        debugger;
        console.log($scope.PaperTLMAM);

        for (var i in $scope.PaperTLMAM) {
            count = $scope.PaperTLMAM[i].NoOfCredits + count;
            count1 = $scope.PaperTLMAM[i].AssessmentMethodMarks + count1;

            for (var j in $scope.PaperTLMAM[i].AssessType) {
                total = total + $scope.PaperTLMAM[i].AssessType[j].Max;
            }
           
            if (count1 != total) {
                alert("Your Entered Marks Are More/Less Than What you entered In Assessment ");
                flag = false;
                break;
            }
            else {                
                flag = true;
            }
        }
        if (flag == true) {
            $scope.countcheck.count = count;
            $scope.countcheck.count1 = count1;
            $scope.AddTLMAMMarks();
        }
    };
   
    /* To update/Insert Marks in TeachingLearningPaperMap table */
    $scope.AddTLMAMMarks = function () {
        
        if ($scope.countcheck.count > $scope.MstPaperTeachingLearningMap.NoOfCredits1 || $scope.countcheck.count < $scope.MstPaperTeachingLearningMap.NoOfCredits1)
        {
            alert("Your Entered Credits Are More/Less Than What you entered In Define Paper ");
        }
        else if ($scope.countcheck.count1 > $scope.MstPaperTeachingLearningMap.MaxMarks || $scope.countcheck.count1 < $scope.MstPaperTeachingLearningMap.MaxMarks)
        {
            alert("Your Entered MaxMarks Are More/Less Than What you entered In Define Paper ");
        }
        else {           

            $http({
                method: 'POST',
                url: 'api/MstPaperTeachingLearningMap/MstPaperTLMMarksAdd',
                data: $scope.PaperTLMAM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);

                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}