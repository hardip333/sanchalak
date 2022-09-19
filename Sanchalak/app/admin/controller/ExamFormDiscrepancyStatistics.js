app.controller('ExamFormDiscrepancyStatisticsCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Exam Form Discrepancy Statistics";
    $scope.showTableFlag = true;
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.DisplayApplicantDetails = function (data) {
       
        $scope.ExamCancelForm = data ;
        $scope.showFormFlag = true;
        $scope.showTableFlag = false;
        $scope.Backbtn = true;
        $http({
            method: 'POST',
            url: 'api/ExamFormDiscrepancyStatistics/CancelExamFormApplicantDetails',
            data: $scope.ExamCancelForm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.CancelFromExamApplicantList = response.obj;
                        
                       
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

       
    }

    $scope.checkAll = function () {

        for (var i = 0; i < $scope.CancelFromExamApplicantList.length; i++) {
            $scope.CancelFromExamApplicantList[i].IsCheckSelect = $scope.CancelExamForm.SelAllEval;
        }

    };

    //Check atleast one checkbox
    $scope.AtLeastOneCheckforExamForm = function () {
        var count = 0;
        for (var i = 0; i < $scope.CancelFromExamApplicantList.length; i++) {
            if ($scope.CancelFromExamApplicantList[i].IsCheckSelect == null || $scope.CancelFromExamApplicantList[i].IsCheckSelect === undefined
                || $scope.CancelFromExamApplicantList[i].IsCheckSelect == false
            )
            {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count > 0) {
            $scope.UpdateExamFormDiscrepancyStatistics();

        }
        else {
            alert("Please Select at least one checkbox");
        }

    }

    /*Back to Edit Page of Subject*/
    $scope.backToList = function () {
        $scope.showTableFlag = true;
        $scope.showFormFlag = false;
        $scope.Backbtn = false;
        $scope.CancelExamForm = {};
    }; 

    $scope.UpdateExamFormDiscrepancyStatistics = function (ev) {
        
        var confirm = $mdDialog.confirm()
            .title('Would you like to Cancel Exam Form?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

       
        $scope.CancelExamForm1 = "";
        for (var i = 0; i < $scope.CancelFromExamApplicantList.length; i++) {
            if ($scope.CancelFromExamApplicantList[i].IsCheckSelect) {
                var StudentPRN = $scope.CancelFromExamApplicantList[i].StudentPRN;
                $scope.CancelExamForm1 += + StudentPRN + ",";
            }
        }
       
        $scope.CancelExamForm1 = $scope.CancelExamForm1.slice(0, -1);
        $scope.FinalModel = {};
        $scope.FinalModel.CancelExamForm1 = $scope.CancelExamForm1;
        $mdDialog.show(confirm).then(function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ExamFormDiscrepancyStatistics/UpdateCancelExamFormDiscrepancyStatistics',
                data: $scope.FinalModel,
                eaders: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        //$rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                        
                        
                    }
                    else {
                     
                        $scope.getExamFormCancelApplicantList();
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.showTableFlag = true;
                        $scope.NoRecLabel = false;
                        $scope.showFormFlag = false;
                        $scope.Backbtn = false;
                        $scope.CancelExamForm = {};
                       

                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    }

    $scope.getExamFormCancelApplicantList = function () {
     
        var data = new Object();       
        $http({
            method: 'POST',
            url: 'api/ExamFormDiscrepancyStatistics/CancelExamFormDetailsByProgramme',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
               
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.NoRecLabel = true;
                        $scope.showTableFlag = false;
                    }
                    else {
                       
                       
                       $scope.ExamFormDiscrepancyStatisticsTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                       });
                        $scope.showTableFlag = true;
                        $scope.NoRecLabel = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    


   

   

   


});