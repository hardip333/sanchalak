app.controller('MeritListInstanceCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Merit List Instance";

    $rootScope.showLoading = false;
    $scope.showSeatMatrixListFlag = true;
    $scope.disabledForName = false;
    $scope.disabledForAcademic = false;
    $scope.disabledForFaculty = false;
    $scope.disabledForProgrammeInsPartterm = false;
    $scope.disabledForRound = false;
    $scope.disabledForReferenceMeritlist = false;
    $scope.disabledForPreferance = false;

    // for Merit Instance
    $scope.editInstance = function (data) {
        $scope.instance = data;
        $scope.instance.Round += "";
        $scope.disabledForName = true;
        $scope.disabledForAcademic = true;
        $scope.disabledForFaculty = true;
        $scope.disabledForProgrammeInsPartterm = true;
        $scope.disabledForRound = true;
        $scope.disabledForReferenceMeritlist = true;
        $scope.disabledForPreferance = true;
        $scope.showReferenceMeritListDropdown($scope.instance.Round);
        $scope.getActiveProgrammeInstancePartTermGetByFacIdAndAcadId($scope.instance);
        $scope.getActiveMeritListInstanceGetActive($scope.instance);
    }

    $scope.cancelInstance = function () {
        $scope.instance = {
            ProgrammeInstancePartTermId: 0,
            RefMeritListId: 0,
            AcademicYearId: 0,
            FacultyId: 0
        };
    };

    $scope.cancelInstance();

    $scope.getMeritListInstanceGet = function () {
        $rootScope.showLoading = true;

        var xml = new Object();

        $http({
            method: 'POST',
            url: 'api/MeritListInstance/MeritListInstanceGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.meritListInstanceGet = response.obj;
            
                    $scope.meritListInstanceGetCount2 = $scope.meritListInstanceGet.length;

                    for (var i = 0; i < $scope.meritListInstanceGetCount2; i++) {
                        $scope.meritListInstanceGet[i].ConsiderPreference += "";
                    }

                    var data = $scope.meritListInstanceGet.slice();

                    $scope.instanceTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: data
                    });
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.getMeritListInstanceGet();

    // for save
    $scope.saveInstanceAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.instance = data;

        //let stringValue = $scope.instance.ConsiderPreference ;
        //let boolValue = (stringValue == $scope.instance.ConsiderPreference);
        //$scope.instance.ConsiderPreference = boolValue;
        //alert($scope.instance.ConsiderPreference);

        $http({
            method: 'POST',
            url: 'api/MeritListInstance/MeritListInstanceAdd',
            data: $scope.instance,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);

                }
                else {
                    alert(response.obj);
                    $scope.cancelInstance();
                    $scope.getMeritListInstanceGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
                /*    alert(res.obj);*/
            });
    };

    // for edit with api 
    $scope.editInstanceEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.instance = data;
        //alert($scope.instance.ConsiderPreference);
        //if ($scope.instance.ConsiderPreference === true) {

        //    $scope.instance.ConsiderPreference = 1;
        //    alert(1);
        //    alert($scope.instance.ConsiderPreference);
        //} else if ($scope.instance.ConsiderPreference === false) {
        //    $scope.instance.ConsiderPreference = 0;
        //    alert(2);
        //    alert($scope.instance.ConsiderPreference);
        //}
        $http({
            method: 'POST',
            url: 'api/MeritListInstance/MeritListInstanceEdit',
            data: $scope.instance,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getMeritListInstanceGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save examcenter with condition
    $scope.saveInstance = function (data) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveInstanceAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editInstanceEdit(data);

        }

    }

    //$scope.hideInstance = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/MeritListInstance/MeritListInstanceIsActive',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getMeritListInstanceGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });

    //};

    //$scope.showInstance = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/MeritListInstance/MeritListInstanceIsInactive',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getMeritListInstanceGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // show refernece merit list dropdown
    $scope.showReferenceMeritListDropdown = function (data) {
        if (data == 1) {
            $scope.showReferenceMeritListFlag = false;

        } else {
            $scope.showReferenceMeritListFlag = true;
            $scope.getActiveMeritListInstanceGetActive($scope.instance);
        }
    }

    // for meritlist configuration
    // show fix or field view of denominator for education parameter

    $scope.showDenominatorValueFlag = false;
    $scope.showDenominatorFieldFlag = false;
    $scope.RadioChangeForEducationParameter = function (data,s) {
        $scope.denominatorSelected = s;
        $scope.education = data;
        if ($scope.denominatorSelected == false) {
            data.showDenominatorValueFlag = true;
          /*  data.showDenominatorFieldFlag = false;*/
        }
        //else if ($scope.denominatorSelected == false) {
 
        //    data.showDenominatorFieldFlag = true;
        //    data.showDenominatorValueFlag = false;
        //}
    };

    // for info button
    $scope.showMeritlistConfiguration = function (data) {
        $scope.showMeritlistConfigurationFlag = true;
        $scope.getMeritListEducationParameterConfigurationGetList(data);
        $scope.getMeritListAddOnQuestionParameterConfigurationGetList(data);
        $scope.getMeritListManualParameterConfigurationGetList(data);
        $scope.getActiveMstPreferenceGroupGetByProgInstPartTermIdList(data);
        $scope.getActiveAdmProgrammeAddOnCriteriaGetAll(data);
 /*       $scope.questionparameter = data2;*/
        $scope.getActiveAdmProgrammeAddOnCriteriaForMeritList(data);
        $scope.getSeatMatrixListGetList(data);
        $scope.getMeritlistSpecialCategoryConfigurationGetList(data);
        $rootScope.SeatMatrixLocked2 = data.SeatMatrixLocked;
        $rootScope.ConfigurationLocked2 = data.ConfigurationLocked;
        $scope.selectedMeritInstance = data;
     
    }
     // for close info button 1
    $scope.cancelMeritlistConfiguration = function () {
        $scope.showMeritlistConfigurationFlag = false;

    };

    // for prefernce dropdown
    $scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = [];
    $scope.defaultPreferanceList = [];
    //var preObj = {
    //    Id: 0,
    //    GroupName: "Select Preferance"
    //}
  /*  $scope.defaultPreferanceList.push(preObj);*/
    $scope.getActiveMstPreferenceGroupGetByProgInstPartTermIdList = function (data) {

        $http({
            method: 'POST',
            url: 'api/MstPreferenceGroup/MstPreferenceGroupGetByProgInstPartTermId',
            data: { ProgrammeInstancePartTermId : data.ProgrammeInstancePartTermId},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.tempList = response.obj;
                    $scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = $scope.defaultPreferanceList.concat($scope.tempList);
                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    }
  /*  $scope.getActiveMstPreferenceGroupGetByProgInstPartTermIdList();*/
    // For get Mst Groupcode Start

    $scope.GetPreferenceGroupCode = function () {

        $http({
            method: 'POST',
            url: 'api/MstGroupCode/MstGroupCodeGet',
            //data: { ProgrammeInstancePartTermId: data.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.CodeList = response.obj;
                   //$scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = $scope.defaultPreferanceList.concat($scope.tempList);
                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    }
    $scope.GetPreferenceGroupCode();

    // For get Mst Groupcode End
    // for add on questions dropdown 
    $scope.activeAdmProgrammeAddOnCriteriaGetAll = [];
    $scope.defaultAddonList = [];
    var addonObj = {
        Id: 0,
        TitleName: "Select Add On-Question"
    }
    $scope.defaultAddonList.push(addonObj);

    $scope.getActiveAdmProgrammeAddOnCriteriaGetAll = function (data) {
      
        $http({
            method: 'POST',
            url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaForMeritList',
            data: { ProgrammeInstancePartTermId: data.ProgrammeInstancePartTermId},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.tempList = response.obj;
                    $scope.activeAdmProgrammeAddOnCriteriaGetAll = $scope.defaultAddonList.concat($scope.tempList);
                    //for (var i = 0; i < $scope.activeAdmProgrammeAddOnCriteriaGetAll.length; i++) {
                    //    $scope.activeAdmProgrammeAddOnCriteriaGetAll[i].Id = $scope.filter.addonId;
                    //    alert($scope.activeAdmProgrammeAddOnCriteriaGetAll[i].Id);
                    //}
                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    }
  /*  $scope.getActiveAdmProgrammeAddOnCriteriaGetAll();*/

    // for add on questions dropdown based on meritlist
    $scope.activeAdmProgrammeAddOnCriteriaForMeritList = [];
    $scope.defaultCriteriaList = [];
    var addonObj = {
        Id: 0,
        TitleName: "Select Add On-Question"
    }
    $scope.defaultCriteriaList.push(addonObj);

    $scope.getActiveAdmProgrammeAddOnCriteriaForMeritList = function (data,data2) {
   
        $http({
            method: 'POST',
            url: 'api/AdmProgrammeAddOnCriteria/AdmProgrammeAddOnCriteriaForMeritList',
            data: { ProgrammeInstancePartTermId: data.ProgrammeInstancePartTermId, Id: data2},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.tempList = response.obj;
                    $scope.activeAdmProgrammeAddOnCriteriaForMeritList = $scope.defaultCriteriaList.concat($scope.tempList);
                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    }
  /*  $scope.getActiveAdmProgrammeAddOnCriteriaForMeritList();*/

    // for get education parameter
    $scope.getMeritListEducationParameterConfigurationGetList = function (data) {
     
        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListInstanceId = data.Id;
        $scope.MeritListInstanceId7 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/MeritListEducationParameterConfiguration/MeritListEducationParameterConfigurationGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.meritListEducationParameterConfigurationGetList = response.obj;
                    for (var i = 0; i < $scope.meritListEducationParameterConfigurationGetList.length ; i++) {
                        $scope.meritListEducationParameterConfigurationGetList[i].DenominatorType += "";
                    }
                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

      // for edit education parameter with condition
    $scope.educationsFieldsFlag = false;
    $scope.editEducationParameters = function (data) {
        data.educationsFieldsFlag = true;

        if (data.DenominatorType === 'true') {
            data.showDenominatorValueFlag = true;
        } else if (data.DenominatorType === 'false') {
            data.showDenominatorFieldFlag = true;
        }
    }

    // for save education parameter with condition
    $scope.saveEducationParameters = function (data) {
        $scope.educationsFieldsFlag = true;
   
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveMeritListEducationParameterConfigurationAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editMeritListEducationParameterConfigurationEdit(data);
        }
    }

    // for edit education parameter
    $scope.editMeritListEducationParameterConfigurationEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.educationsFieldsFlag = true;
        $scope.education = data;
        data.MeritListInstanceId = $scope.MeritListInstanceId7;
     
       
        $http({
            method: 'POST',
            url: 'api/MeritListEducationParameterConfiguration/MeritListEducationParameterConfigurationEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                
                    $scope.getMeritListEducationParameterConfigurationGetList($scope.selectedMeritInstance);
                    $scope.educationsFieldsFlag = false;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save education parameter
    $scope.saveMeritListEducationParameterConfigurationAdd = function (data) {
  
        $rootScope.showLoading = true;
        $scope.educationsFieldsFlag = true;
        $scope.education = data;
        $scope.education.MeritListInstanceId = $scope.MeritListInstanceId7;

        $http({
            method: 'POST',
            url: 'api/MeritListEducationParameterConfiguration/MeritListEducationParameterConfigurationAdd',
            data: $scope.education,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                   
                    $scope.getMeritListEducationParameterConfigurationGetList($scope.selectedMeritInstance);
                    $scope.educationsFieldsFlag = true;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

     // delete education parameter
    $scope.deleteEducationParameters = function (data,index) {
        $scope.educationsFieldsFlag = true;
    
        if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteEducationParametersDynamic(data);
        } else if (data.Id === undefined || data.Id === null || data.Id === '' ) {
            $scope.deleteEducationParametersStatic(index);
        }
    }

    $scope.deleteEducationParametersStatic = function (index) {

        var confirm1 = $mdDialog.confirm()
            .title('Would You Delete Education Parameter ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm1).then(function () {
            $scope.meritListEducationParameterConfigurationGetList.splice(index, 1);
        }, function () {
        });
        $scope.educationsFieldsFlag = false;
    }

    $scope.deleteEducationParametersDynamic = function (data) {

        var confirm2 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Education Parameter?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm2).then(function () {
    
            $http({
                method: 'POST',
                url: 'api/MeritListEducationParameterConfiguration/MeritListEducationParameterConfigurationDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getMeritListEducationParameterConfigurationGetList($scope.selectedMeritInstance);
                        $scope.educationsFieldsFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


        }, function () {

        });
    };

    // for add education parameter
    $scope.addEducationFieldsListToConfuguration = function (index) {
        var education = {
            educationsFieldsFlag: true,
            FieldName : "",
            level: "",
            Weightage : "",
            DenominatorType : "",
            DenominatorValue : "",
            DenominatorField : "",
            Sequence : "",
        };

        $scope.meritListEducationParameterConfigurationGetList.push(education);
    };


    // for blank array of education parameter
    $scope.meritListEducationParameterConfigurationGetList = [
        {
            FieldName: "",
            level: "",
            Weightage: "",
            DenominatorType: "",
            DenominatorValue: "",
            DenominatorField: "",
            Sequence: "",
        }];

    // for AddOn Question Parameter 
    // for get AddOn Question Parameter 
    $scope.getMeritListAddOnQuestionParameterConfigurationGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListInstanceId = data.Id;
        $scope.MeritListInstanceId2 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/MeritListAddOnQuestionParameterConfiguration/MeritListAddOnQuestionParameterConfigurationGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.meritListAddOnQuestionParameterConfigurationGetList = response.obj;
                    for (var j = 0; j < $scope.meritListAddOnQuestionParameterConfigurationGetList.length; j++) {
                            $scope.meritListAddOnQuestionParameterConfigurationGetList[j].DenominatorType += "";
                        }
                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit AddOn Question Parameter  with condition
    $scope.questionParameterFlag = false;
    $scope.editQuestionParameters = function (data) {

        data.questionParameterFlag = true;

        if (data.DenominatorType === 'true') {
            data.showDenominatorValueFoeAddonQuestionsFlag = true;
        } else if (data.DenominatorType === 'false') {
            data.showDenominatorFieldFoeAddonQuestionsFlag = true;
        }
    }

    // for save AddOn Question Parameter  with condition
    $scope.saveQuestionParameters = function (data) {
        $scope.questionParameterFlag = true;
   
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveMeritListAddOnQuestionParameterConfigurationAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editMeritListAddOnQuestionParameterConfigurationEdit(data);
        }

    }

    // for edit AddOn Question Parameter 
    $scope.editMeritListAddOnQuestionParameterConfigurationEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.questionParameterFlag = true;
        $scope.questionparameter = data;

        $scope.questionparameter.MeritListInstanceId = $scope.MeritListInstanceId2;

        $http({
            method: 'POST',
            url: 'api/MeritListAddOnQuestionParameterConfiguration/MeritListAddOnQuestionParameterConfigurationEdit',
            data: $scope.questionparameter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.questionParameterFlag = true;
                    $scope.getMeritListAddOnQuestionParameterConfigurationGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save AddOn Question Parameter 
    $scope.saveMeritListAddOnQuestionParameterConfigurationAdd = function (data) {

        $rootScope.showLoading = true;
        $scope.questionParameterFlag = true;
        $scope.questionparameter = data;
        $scope.questionparameter.MeritListInstanceId = $scope.MeritListInstanceId2;

        $http({
            method: 'POST',
            url: 'api/MeritListAddOnQuestionParameterConfiguration/MeritListAddOnQuestionParameterConfigurationAdd',
            data: $scope.questionparameter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.questionParameterFlag = false;
                    $scope.getMeritListAddOnQuestionParameterConfigurationGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // delete AddOn Question Parameter 
    $scope.deleteQuestionParameters = function (data,index) {
        $scope.questionParameterFlag = true;

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.deleteQuestionParametersStatic(index);
        } else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteQuestionParametersDynamic(data);
        }

    }

    $scope.deleteQuestionParametersStatic = function (index) {
   
        var confirm3 = $mdDialog.confirm()
            .title('Would You Delete Add On Question Parameter ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm3).then(function () {
            $scope.meritListAddOnQuestionParameterConfigurationGetList.splice(index, 1);
        }, function () {
        });
        $scope.questionParameterFlag = false;
    }

    $scope.deleteQuestionParametersDynamic = function (data) {

        var confirm4 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Add On Question Parameter ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm4).then(function () {

            $http({
                method: 'POST',
                url: 'api/MeritListAddOnQuestionParameterConfiguration/MeritListAddOnQuestionParameterConfigurationDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.questionParameterFlag = false;
                        $scope.getMeritListAddOnQuestionParameterConfigurationGetList($scope.selectedMeritInstance);
                     
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


        }, function () {

        });
    };

    // for add AddOn Question Parameter 
    $scope.addQuestionParameters = function (index) {
        var questionparameter = {
            questionParameterFlag: true,
            AddOnQuestionId: "",
            Weightage: "",
            DenominatorType: "",
            DenominatorValue: "",
            DenominatorFieldId: "",
            Sequence: "",
        };
     $scope.meritListAddOnQuestionParameterConfigurationGetList.push(questionparameter);
    };

    // for blank array of AddOn Question Parameter 
    $scope.meritListAddOnQuestionParameterConfigurationGetList = [
        {
            AddOnQuestionId: "",
            Weightage: "",
            DenominatorType: "",
            DenominatorValue: "",
            DenominatorFieldId: "",
            Sequence: "",
        }];

    // show fix or field view of denominator for education parameter
    $scope.showDenominatorValueFoeAddonQuestionsFlag = false;
    $scope.showDenominatorFieldFoeAddonQuestionsFlag = false;
    $scope.RadioChangeForQuestionParameter = function (data, s2) {
        $scope.denominatorSelectedForquestionParameter = s2;
        $scope.questionparameter = data;
        if ($scope.denominatorSelectedForquestionParameter == true) {
            data.showDenominatorValueFoeAddonQuestionsFlag = true;
            data.showDenominatorFieldFoeAddonQuestionsFlag = false;
        }
        else if ($scope.denominatorSelectedForquestionParameter == false) {
            $scope.getActiveAdmProgrammeAddOnCriteriaForMeritList($scope.selectedMeritInstance, $scope.questionparameter.AddOnQuestionId);
            data.showDenominatorFieldFoeAddonQuestionsFlag = true;
            data.showDenominatorValueFoeAddonQuestionsFlag = false;
        }
    };

    // for Manual Parameter
    // for get Manual Parameter
    $scope.getMeritListManualParameterConfigurationGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListInstanceId = data.Id;
        $scope.MeritListInstanceId3 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/MeritListManualParameterConfiguration/MeritListManualParameterConfigurationGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.meritListManualParameterConfigurationGetList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit Manual Parameter  with condition
    $scope.manualParametersFlag = false;
    $scope.editManualParameters = function (data) {
        data.manualParametersFlag = true;
    }

    // for save Manual Parameter with condition
    $scope.saveManualParameters = function (data) {
        $scope.manualParametersFlag = true;

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveMeritListManualParameterConfigurationAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editMeritListManualParameterConfigurationEdit(data);
        }
    }

    // for edit Manual Parameter 
    $scope.editMeritListManualParameterConfigurationEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.manualParametersFlag = true;
        $scope.manualparameter = data;
        $scope.manualparameter.MeritListInstanceId = $scope.MeritListInstanceId3;

        $http({
            method: 'POST',
            url: 'api/MeritListManualParameterConfiguration/MeritListManualParameterConfigurationEdit',
            data: $scope.manualparameter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.manualParametersFlag = true;
                    $scope.getMeritListManualParameterConfigurationGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save ManualParameter
    $scope.saveMeritListManualParameterConfigurationAdd = function (data) {

        $rootScope.showLoading = true;
        $scope.manualParametersFlag = true;
        $scope.manualparameter = data;
        $scope.manualparameter.MeritListInstanceId = $scope.MeritListInstanceId3;

        $http({
            method: 'POST',
            url: 'api/MeritListManualParameterConfiguration/MeritListManualParameterConfigurationAdd',
            data: $scope.manualparameter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.manualParametersFlag = false;
                    $scope.getMeritListManualParameterConfigurationGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // delete Manual Parameter
    $scope.deleteManualParameters = function (data,index) {
        $scope.manualParametersFlag = true;

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.deleteManualParametersStatic(index);
        } else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteManualParametersDynamic(data);
        }
    }

    $scope.deleteManualParametersStatic = function (index) {

        var confirm5 = $mdDialog.confirm()
            .title('Would You Delete Manual Parameter ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm5).then(function () {
            $scope.meritListManualParameterConfigurationGetList.splice(index, 1);
        }, function () {
        });
        $scope.manualParametersFlag = false;
    }

    $scope.deleteManualParametersDynamic = function (data) {

        var confirm6 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Manual Parameter ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm6).then(function () {

            $http({
                method: 'POST',
                url: 'api/MeritListManualParameterConfiguration/MeritListManualParameterConfigurationDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getMeritListManualParameterConfigurationGetList($scope.selectedMeritInstance);
                        $scope.manualParametersFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }, function () {

        });
    };

    // for add Manual Parameter
    $scope.addManualParameters = function (index) {
        var manualparameter = {
            manualParametersFlag: true,
            DisplayName: "",
            Weightage: "",
            Denominator: "",
            Sequence: "",
        };
        $scope.meritListManualParameterConfigurationGetList.push(manualparameter);
    };

    // for blank array of ManualParameter
    $scope.meritListManualParameterConfigurationGetList = [
        {
            DisplayName: "",
            Weightage: "",
            Denominator: "",
            Sequence: "",
        }];

    // lock configuration
    $scope.lockUpdateConfigurationLocked = function () {
        $rootScope.showLoading = true;

        var xml = new Object();
        xml.Id = $scope.MeritListInstanceId3;

        $http({
            method: 'POST',
            url: 'api/MeritListInstance/UpdateConfigurationLocked',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
   
                }
                else {
                    alert(response.obj);
                    /*  $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);*/
                    $scope.showMeritlistConfigurationFlag = false;
                    $scope.getMeritListInstanceGet();
                    $scope.getMeritListEducationParameterConfigurationGetList($scope.selectedMeritInstance);
                    $scope.getMeritListManualParameterConfigurationGetList($scope.selectedMeritInstance);
                    $scope.getMeritListAddOnQuestionParameterConfigurationGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    /* for tab 2 seat matrix */
    // for seat matrix
    $scope.showBackToInstanceButtonFlag = true;
    $scope.showSeatMatrixSaveButtonFlag = true;
    $scope.showSeatMatrixEditButtonFlag = false;
    $scope.showSpecialSaveButtonFlag = true;
    $scope.showAllowedSaveButtonFlag = true;
    $scope.showSeatMatrixGenderFlag = false;
    $scope.showPrefernceListAndGenderFlag = false;

    $scope.editSeatMatrix = function (data) {
      
        $scope.seatmatrix = data;
          $scope.seatmatrix.Location += "";
        $scope.showSeatMatrixSpecialCategoryMappingFlag = true;
        $scope.showSeatMatrixListFlag = false;
        $scope.showSpecialSaveButtonFlag = true;
        $scope.showAllowedSaveButtonFlag = true;
        $scope.showSeatMatrixEditButtonFlag = true;
        $scope.showSeatMatrixSaveButtonFlag = false;
        $scope.showBackToInstanceButtonFlag = false;
        $scope.showSeatMatrixGenderFlag = true;
        $scope.showPrefernceListAndGenderFlag = false;
        $scope.getSeatMatrixSpecialCategoryMappingListGetList(data);
        $scope.getSeatMatrixAllowedCategoryMappingListGetList(data);

        // convert coma separated string into array with checkbox selected
        $scope.genderUR = [{ id: "Male", checked: false, value: "Male" }, { id: "Female", checked: false, value: "Female" }];
        $scope.csv = $scope.seatmatrix.StrGender;
 
        ////If you REALLY need it as a string
        $scope.csv1 = $scope.csv.split(',').map(Number);

        for (var i = 0; i < $scope.genderUR.length; i++) {
            if ($scope.seatmatrix.StrGender.includes($scope.genderUR[i].value)) {
                $scope.genderUR[i].checked = true;
            }

        }

    }

    $scope.cancelSeatMatrix = function () {
        $scope.seatmatrix = {
            CategoryId: 0,
            Location: 0,
            PreferenceId:0
        };
    };
    $scope.cancelSeatMatrix();

    $scope.getSeatMatrixListGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListInstanceId = data.Id;
        $scope.MeritListInstanceId4 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/SeatMatrix/SeatMatrixListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.seatMatrixListGetList = response.obj;
    /*                var len2 = $scope.seatMatrixListGetList.length;*/

                    for (var i = 0; i < $scope.seatMatrixListGetList.length; i++) {

                        if ($scope.seatMatrixListGetList[i].Location === 1) {

                            $scope.seatMatrixListGetList[i].LocationName = "Vadodara";
                        }

                        else if ($scope.seatMatrixListGetList[i].Location === 2) {

                            $scope.seatMatrixListGetList[i].LocationName = "Out-Side Vadodara";
                        }

                        else if ($scope.seatMatrixListGetList[i].Location === 3) {

                            $scope.seatMatrixListGetList[i].LocationName = "Out-Side Gujarat";
                        }

                    }


                    $scope.seatmatrixTableParams = new NgTableParams({
                    }, {
                        dataset: $scope.seatMatrixListGetList
                    });
                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.saveSeatMatrixAddWithList = function (data) {
        $rootScope.showLoading = true;
     
        $scope.genderForSaveSeatMatrix = [{ id: "Male", isChecked: false, value: "Male" }, { id: "Female", isChecked: false, value: "Female" }];
        $scope.genderSelectionForSave = function () {
            $scope.SelectedGenderForSave = "";
            for (var i = 0; i < $scope.genderForSaveSeatMatrix.length; i++) {
                if ($scope.genderForSaveSeatMatrix[i].isChecked == true) {
                    if ($scope.SelectedGenderForSave == "") {
                        $scope.SelectedGenderForSave = $scope.genderForSaveSeatMatrix[i].id;
                        data.StrGender = $scope.SelectedGenderForSave;

                    } else {
                        $scope.SelectedGenderForSave = $scope.SelectedGenderForSave + ", " + $scope.genderForSaveSeatMatrix[i].id;
                       data.StrGender = $scope.SelectedGenderForSave;
                    }
                }
            }
        }


        data.SeatMatrixCatMapList = $scope.seatMatrixSpecialCategoryMappingList;
        data.SeatMatrixAllowedCatList = $scope.seatMatrixAllowedCategoryMappingListGetList;
        data.MeritListInstanceId = $scope.MeritListInstanceId4;
        //data.EWSReservationPercentage = $scope.seatmatrix.EWSReservationPercentage;
        //data.PhysicallyHandicapReservationPercentage = $scope.seatmatrix.PhysicallyHandicapReservationPercentage;
        data.SeatMatrixPrefernceList = $scope.activeMstPreferenceGroupGetByProgInstPartTermIdList;
        data.StrGender = $scope.SelectedGenderForSave;


        $http({
            method: 'POST',
            url: 'api/SeatMatrix/SeatMatrixAddWithList',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);
                    $scope.showSeatMatrixSpecialCategoryMappingFlag = false;
                    $scope.showSeatMatrixListFlag = true;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 

    $scope.editSeatMatrixEdit = function (data) {

        $rootScope.showLoading = true;
     
        $scope.seatmatrix = data;
 
        //$scope.genderSelection = function () {
             // convert coma separated string  with checkbox selected
        $scope.URSelected = "";
            for (var i = 0; i < $scope.genderUR.length; i++) {
                if ($scope.genderUR[i].checked == true) {
                    if ($scope.URSelected == "") {
                        $scope.URSelected = $scope.genderUR[i].id;
                        data.StrGender = $scope.URSelected;
                     
                    } else {
                        $scope.URSelected = $scope.URSelected + ", " + $scope.genderUR[i].id;
                        data.StrGender = $scope.URSelected;
                    }
                }
            }
        //}
        data.StrGender = $scope.URSelected;

        $http({
            method: 'POST',
            url: 'api/SeatMatrix/SeatMatrixEdit',
            data: $scope.seatmatrix,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);
                    $scope.showSeatMatrixSpecialCategoryMappingFlag = false;
                    $scope.showSeatMatrixListFlag = true;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
      // lock seat matrix
    $scope.lockUpdateSeatMatrixLocked = function (data) {
        $rootScope.showLoading = true;
        data.Id = $scope.MeritListInstanceId4;

        $http({
            method: 'POST',
            url: 'api/MeritListInstance/UpdateSeatMatrixLocked',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    //$scope.hideSeatMatrix = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/SeatMatrix/SeatMatrixIsActive',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    //$scope.showSeatMatrixForList = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/SeatMatrix/SeatMatrixIsInactive',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getSeatMatrixListGetList($scope.selectedMeritInstance);
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // for Seat Matrix Special Category Mapping
    /*  for add button*/
    $scope.showSeatMatrixSpecialCategoryMapping = function () {
        $scope.showSeatMatrixSpecialCategoryMappingFlag = true;
        $scope.showSeatMatrixListFlag = false;
        $scope.showSpecialSaveButtonFlag = false;
        $scope.showAllowedSaveButtonFlag = false;
        $scope.showSeatMatrixEditButtonFlag = false;
        $scope.showSeatMatrixGenderFlag = false;
        $scope.showPrefernceListAndGenderFlag = true;
        $scope.showSeatMatrixSaveButtonFlag = true;
        $scope.showBackToInstanceButtonFlag = false;

        $scope.seatmatrix = {
            CategoryId: 0,
            Location: '',
            PreferenceId:0

        };
        $scope.seatMatrixSpecialCategoryMappingList = [
            {
                ReservationPercentage: "",
                SocialCategoryId: "0"
            }];


        $scope.seatMatrixAllowedCategoryMappingListGetList = [
            {
                CategoryId: "0"
            }];

        //$scope.activeMstPreferenceGroupGetByProgInstPartTermIdList = [
        //    {
        //    IsChecked:"",
        //        GroupName: "",
        //        TotalSeats:"",

        //    }];

        $scope.genderForSaveSeatMatrix = [{ id: "Male", isChecked: false, value: "Male" }, { id: "Female", isChecked: false, value: "Female" }];
        $scope.genderSelectionForSave = function () {
            $scope.SelectedGenderForSave = "";
            for (var i = 0; i < $scope.genderForSaveSeatMatrix.length; i++) {
                if ($scope.genderForSaveSeatMatrix[i].isChecked == true) {
                    if ($scope.SelectedGenderForSave == "") {
                        $scope.SelectedGenderForSave = $scope.genderForSaveSeatMatrix[i].id;

                    } else {
                        $scope.SelectedGenderForSave = $scope.SelectedGenderForSave + ", " + $scope.genderForSaveSeatMatrix[i].id;
               
                    }
                }
            }
        }
    }

    $scope.cancelSeatMatrixSpecialCategoryMappingForCloseButton = function () {
        $scope.showSeatMatrixSpecialCategoryMappingFlag = false;
        $scope.showSeatMatrixListFlag = true;
        $scope.showBackToInstanceButtonFlag = true;
    
    };

  /*  Special Category Mapping*/
    $scope.getSeatMatrixSpecialCategoryMappingListGetList = function (data) {
        $rootScope.showLoading = true;

        $scope.selectedSeatMatrix = data;
        var xml = new Object();

        xml.SeatMatrixId = data.Id;
        $scope.SeatMatrixId = data.Id;

        $http({
            method: 'POST',
            url: 'api/SeatMatrixSpecialCategoryMapping/SeatMatrixSpecialCategoryMappingListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.seatMatrixSpecialCategoryMappingList = response.obj;
                 /*   $scope.seatMatrixSpecialCategoryMappingListCount = $scope.seatMatrixSpecialCategoryMappingList.length;*/
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.cancelSeatMatrixSpecialCategoryMapping = function () {
        $scope.specialcategory = {

        };

    };
/*    $scope.cancelSeatMatrixSpecialCategoryMapping();*/

    // for save
    $scope.saveSeatMatrixSpecialCategoryMappingAdd = function (data) {
        $rootScope.showLoading = true;
        data.SeatMatrixId = $scope.SeatMatrixId;

        $http({
            method: 'POST',
            url: 'api/SeatMatrixSpecialCategoryMapping/SeatMatrixSpecialCategoryMappingAdd',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);

                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixSpecialCategoryMappingListGetList($scope.selectedSeatMatrix);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
                /*    alert(res.obj);*/
            });
    };

    // for edit with api 
    $scope.editSeatMatrixSpecialCategoryMappingEdit = function (data) {

        $rootScope.showLoading = true;
        data.SeatMatrixId = $scope.SeatMatrixId;
  
        $http({
            method: 'POST',
            url: 'api/SeatMatrixSpecialCategoryMapping/SeatMatrixSpecialCategoryMappingEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixSpecialCategoryMappingListGetList($scope.selectedSeatMatrix);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Seat Matrix Special Category with condition
    $scope.saveSpecialCategoryMappingToSeatMatrix = function (data) {
        $rootScope.showLoading = true;
        $scope.specialcategory = data;
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveSeatMatrixSpecialCategoryMappingAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editSeatMatrixSpecialCategoryMappingEdit(data);
        }
   
    }

    /*     remove Seat Matrix Special Category Mapping*/
    $scope.deleteSpecialCategoryMappingToSeatMatrix = function (data, index) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.deleteSpecialCategoryMappingToSeatMatrixStatic(index);
        } else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteSpecialCategoryMappingToSeatMatrixDynamic(data);
        }
    }

    $scope.deleteSpecialCategoryMappingToSeatMatrixStatic = function (index) {

        var confirm6 = $mdDialog.confirm()
            .title('Would You Delete Seat Matrix Special Category ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm6).then(function () {
            $scope.seatMatrixSpecialCategoryMappingList.splice(index, 1);
        }, function () {
        });
     
    }

    $scope.deleteSpecialCategoryMappingToSeatMatrixDynamic = function (data) {

        var confirm7 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Seat Matrix Special Category?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm7).then(function () {

            $http({
                method: 'POST',
                url: 'api/SeatMatrixSpecialCategoryMapping/SeatMatrixSpecialCategoryMappingDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getSeatMatrixSpecialCategoryMappingListGetList($scope.selectedSeatMatrix);
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


        }, function () {

        });
    };

    /*  Add Seat Matrix Special Category Mapping*/
    /*  $scope.specialCategoryActionFlag = true ;*/
    $scope.addSpecialCategoryMappingToSeatMatrix = function (index) {
        var specialcategory = {
            ReservationPercentage: "",
            SocialCategoryId: "0"
        };
        $scope.seatMatrixSpecialCategoryMappingList.push(specialcategory);

    };

    $scope.seatMatrixSpecialCategoryMappingList = [
        {
            ReservationPercentage: "",
            SocialCategoryId:"0"
        }];

    // for get allowed category mapping
    $scope.getSeatMatrixAllowedCategoryMappingListGetList = function (data) {
        $rootScope.showLoading = true;

        $scope.selectedSeatMatrix = data;
        var xml = new Object();

        xml.SeatMatrixId = data.Id;
        $scope.SeatMatrixId2 = data.Id;

        $http({
            method: 'POST',
            url: 'api/SeatMatrixAllowedCategoryMapping/SeatMatrixAllowedCategoryMappingListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.seatMatrixAllowedCategoryMappingListGetList = response.obj;
                    $scope.seatMatrixAllowedCategoryMappingListGetListCount = $scope.seatMatrixAllowedCategoryMappingListGetList.length;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    //$scope.cancelSeatMatrixSpecialCategoryMapping = function () {
    //    $scope.specialcategory = {

    //    };

    //};
    /*    $scope.cancelSeatMatrixSpecialCategoryMapping();*/

    // for save  allowed category mapping
    $scope.saveSeatMatrixAllowedCategoryMappingAdd = function (data) {
        $rootScope.showLoading = true;
        data.SeatMatrixId = $scope.SeatMatrixId2;

        $http({
            method: 'POST',
            url: 'api/SeatMatrixAllowedCategoryMapping/SeatMatrixAllowedCategoryMappingAdd',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);

                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixAllowedCategoryMappingListGetList($scope.selectedSeatMatrix);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
                /*    alert(res.obj);*/
            });
    };

    // for edit  allowed category mapping
    $scope.editSeatMatrixAllowedCategoryMappingEdit = function (data) {

        $rootScope.showLoading = true;
        data.SeatMatrixId = $scope.SeatMatrixId2;

        $http({
            method: 'POST',
            url: 'api/SeatMatrixAllowedCategoryMapping/SeatMatrixAllowedCategoryMappingEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSeatMatrixAllowedCategoryMappingListGetList($scope.selectedSeatMatrix);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Seat Matrix allowed category mapping with condition
    $scope.saveAllowedCategoryMappingToSeatMatrix = function (data) {
        $rootScope.showLoading = true;
        $scope.allowedcategory = data;
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveSeatMatrixAllowedCategoryMappingAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editSeatMatrixAllowedCategoryMappingEdit(data);
        }

    }

    /*     remove Seat Matrix allowed category  Mapping*/
    $scope.deleteAllowedCategoryMappingToSeatMatrix = function (data, index) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.deleteAllowedCategoryMappingToSeatMatrixStatic(index);
        } else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteAllowedCategoryMappingToSeatMatrixDynamic(data);
        }
    }

    $scope.deleteAllowedCategoryMappingToSeatMatrixStatic = function (index) {

        var confirm8 = $mdDialog.confirm()
            .title('Would You Delete Seat Matrix Allowed Category ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm8).then(function () {
            $scope.seatMatrixAllowedCategoryMappingListGetList.splice(index, 1);
        }, function () {
        });

    }

    $scope.deleteAllowedCategoryMappingToSeatMatrixDynamic = function (data) {

        var confirm9 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Seat Matrix Allowed Category?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm9).then(function () {

            $http({
                method: 'POST',
                url: 'api/SeatMatrixAllowedCategoryMapping/SeatMatrixAllowedCategoryMappingDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getSeatMatrixAllowedCategoryMappingListGetList($scope.selectedSeatMatrix);
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


        }, function () {

        });
    };

    /*  Add Seat Matrix allowed category Mapping*/
    /*  $scope.specialCategoryActionFlag = true ;*/
    $scope.addAllowedCategoryMappingToSeatMatrix = function (index) {
        var allowedcategory = {
            CategoryId: "0"
        };
        $scope.seatMatrixAllowedCategoryMappingListGetList.push(allowedcategory);

    };

    $scope.seatMatrixAllowedCategoryMappingListGetList = [
        {
            CategoryId: "0"
        }];

    // for Provisional List button
    $scope.showProvisionalList = function (data) {
        $scope.getProvisionalMeritListListGetList(data);
        $scope.showProvisionalListFlag = true;

        $scope.selectedMeritInstance = data;
    }

    // for close Provisional List button 
    $scope.cancelProvisionalList = function () {
        $scope.showProvisionalListFlag = false;

        $scope.selectedMeritInstance = {};
    };

    $scope.showUserName = true;
    $scope.showApplicationFormNumber = true;
    $scope.showGender = true;
    $scope.showActions = true;
    $scope.showIndex = true;
    $scope.showNameAsPerMarksheet = true;

    // for radio button of EWS 
    $scope.RadioChangeForIsEWS = function (data, s3) {
        $scope.isEWSSelected = s3;
        $scope.provisional = data;
        if ($scope.isEWSSelected == true) {
        }
        else if ($scope.isEWSSelected == false) {
        }
    };

    // for radio button of IsPhysicallyChallanged
    $scope.RadioChangeForIsPhysicallyChallanged = function (data, s4) {
        $scope.isPhysicallyChallangedSelected = s4;
        $scope.provisional = data;
        if ($scope.isPhysicallyChallangedSelected == true) {
        }
        else if ($scope.isPhysicallyChallangedSelected == false) {
        }
    };


    // for radio button of LocalToVadodara
    $scope.RadioChangeForLocalToVadodara = function (data, s5) {
        $scope.isLocalToVadodaraSelected = s5;
        $scope.provisional = data;
        if ($scope.isLocalToVadodaraSelected == true) {
        }
        else if ($scope.isLocalToVadodaraSelected == false) {
        }
    };

    // for provisional list get 

    $scope.getProvisionalMeritListListGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListId  = data.Id;
        $scope.MeritListId10 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritList/ProvisionalMeritListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.provisionalMeritListListGetList = response.obj;
                    $scope.provisionalMeritListListCount = $scope.provisionalMeritListListGetList.length;
                    for (var i = 0; i < $scope.provisionalMeritListListCount; i++) {
                        $scope.provisionalMeritListListGetList[i].IsEWS += "";
                        $scope.provisionalMeritListListGetList[i].IsPhysicallyChallanged += "";
                        $scope.provisionalMeritListListGetList[i].LocalToVadodara  += "";
                    }
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for generate provisional list button
    $scope.saveProvisionalMeritListAdd = function () {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListId = $scope.MeritListId10;

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritList/ProvisionalMeritListAdd',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getProvisionalMeritListListGetList($scope.selectedMeritInstance);
                   
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for generate Merit list button
    $scope.saveProvisionalMeritListFinal = function () {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.Id = $scope.MeritListId10;

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritList/ProvisionalMeritListProcess',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    //$scope.getProvisionalMeritListListGetList($scope.selectedMeritInstance);

                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };


    $scope.provisionalFieldsFlag = false;
    $scope.showProvisionallistDetailFlag = false;
    // for edit provisional list with condition
    $scope.provisionalFieldsFlag = false;
    $scope.editProvisionalList = function (data) {
        data.provisionalFieldsFlag = true;
        $scope.provisional = data;
        $scope.showProvisionallistDetailFlag = true;
        $scope.showBackToInstanceButtonFlag = false;
     
        $scope.getProvisionalMeritListAddOnInformationGetList(data);
        $scope.getProvisionalMeritListManualParameterValuesGetList(data);
        $scope.getProvisionalMeritListEducationDetailsGetList(data);
        $scope.selectedProvisional = data;
    }

    $scope.cancelProvisionallistDetail = function (data) {
        $scope.showProvisionallistDetailFlag = false;
        data.provisionalFieldsFlag = false;
        $scope.showProvisionalListFlag = true;
        $scope.showBackToInstanceButtonFlag = true;
    }

    //// for save provisional list with condition
    //$scope.saveProvisionalList = function (data) {
    //    $scope.provisionalFieldsFlag = true;

    //    if (data.Id === undefined || data.Id === null || data.Id === '') {
    //        $scope.saveProvisionalMeritListAdd(data);
    //    }
    //    else if (data.Id !== 0 || data.Id !== undefined) {
    //        $scope.editProvisionalMeritListEdit(data);
    //    }

    //}

    // for provisional list parameter
    $scope.editProvisionalMeritListEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.provisionalFieldsFlag = true;
        $scope.provisional = data;
        data.MeritListId = $scope.MeritListId10;


        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritList/ProvisionalMeritListEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);

                    $scope.getProvisionalMeritListListGetList($scope.selectedMeritInstance);
        /*            $scope.provisionalFieldsFlag = false;*/
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

  





    //// for save provisional list
    //$scope.saveProvisionalMeritListAdd = function (data) {

    //    $rootScope.showLoading = true;
    //    $scope.provisionalFieldsFlag = true;
    //    $scope.provisional = data;
    //    $scope.provisional.MeritListId = $scope.MeritListId10;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalMeritList/ProvisionalMeritListAdd',
    //        data: $scope.provisional,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);

    //                $scope.getProvisionalMeritListListGetList($scope.selectedMeritInstance);
    //                $scope.provisionalFieldsFlag = true;
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};



    //// for add provisional list
    //$scope.addProvisionalList = function (index) {
    //    var provisional = {
    //        provisionalFieldsFlag: true,
    //        UserName: "",
    //        ApplocationFormNo: "",
    //        Gender: "",
    //        NameAsPerMarksheet: "",
    //        ReservationCategory: "",
    //        IsEWS: "",
    //        SocialCategoryId: "",
    //        IsPhysicallyChallanged: "",
    //        LocalToVadodara: "",
    //        LastQualifyingDegreeSchool: "",
    //        LastQualifyingDegreeSchoolCityId: "",
    //        HSCSchool: "",
    //        HSCSchoolCityId: "",
    //        Mobile: "",
    //        EmailId: "",
    //        Address: "",
    //        StateId: ""
    //    };
    //    $scope.provisionalMeritListListGetList.push(provisional);

    //}


    //// for blank array of provisional list
    // $scope.provisionalMeritListListGetList = [
    //        {
    //            UserName: "",
    //            ApplocationFormNo: "",
    //            Gender: "",
    //            NameAsPerMarksheet: "",
    //            ReservationCategory: "",
    //            IsEWS: "",
    //            SocialCategoryId: "",
    //            IsPhysicallyChallanged: "",
    //            LocalToVadodara: "",
    //            LastQualifyingDegreeSchool: "",
    //            LastQualifyingDegreeSchoolCityId: "",
    //            HSCSchool: "",
    //            HSCSchoolCityId: "",
    //            Mobile: "",
    //            EmailId: "",
    //            Address: "",
    //            StateId: ""
    //        }];

    //$scope.teethUR = [{ id: "Male", checked: false, value: "Male" }, { id: "Female", checked: false, value: "Female" }];
    //$scope.upperRight = function (data) {
    //    $scope.seatmatrix = data;
    //    $scope.URSelected = "";
    //    for (var i = 0; i < $scope.teethUR.length; i++) {
    //        if ($scope.teethUR[i].checked == true) {
    //            if ($scope.URSelected == "") {
    //                $scope.URSelected = $scope.teethUR[i].id;
    //            } else {
    //                $scope.URSelected = $scope.URSelected + ", " + $scope.teethUR[i].id;
    //            }
    //        }
    //    }
    //}


 /*   Provisional Meritlist Educational Details*/
    // for get Provisional Meritlist Educational Details
    $scope.getProvisionalMeritListEducationDetailsGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.ProvisionalMeritListId  = data.Id;


        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListEducationDetails/ProvisionalMeritListEducationDetailsGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.provisionalMeritListEducationDetailsGetList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };



    $scope.showProvisionalEducationDetailFlag = false;
    $scope.editProvisionalMeritlistEducationalDetails = function (data) {
        data.showProvisionalEducationDetailFlag = true;
    }

    // for save  Provisional Meritlist Educationalr with condition
    $scope.saveProvisionalMeritlistEducationalDetails = function (data) {
        $scope.showProvisionalEducationDetailFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.saveProvisionalMeritListEducationDetailsAdd(data);
        //}
         if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editProvisionalMeritListEducationDetailsEdit(data);
        }

    }

    // for edit  Provisional Meritlist Educational details
    $scope.editProvisionalMeritListEducationDetailsEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.showProvisionalEducationDetailFlag = true;
        $scope.provisionaleducational = data;


        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListEducationDetails/ProvisionalMeritListEducationDetailsEdit',
            data: $scope.provisionaleducational,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.showProvisionalEducationDetailFlag = true;
                    $scope.getProvisionalMeritListEducationDetailsGetList($scope.selectedProvisional);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Provisional Meritlist Educational details
    //$scope.saveProvisionalMeritListEducationDetailsAdd= function (data) {

    //    $rootScope.showLoading = true;
    //    $scope.showProvisionalEducationDetailFlag = true;
    //    $scope.provisionaleducational = data;
    ///*    $scope.provisionaleducational.MeritListInstanceId = $scope.MeritListInstanceId3;*/

    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalMeritListEducationDetails/ProvisionalMeritListEducationDetailsAdd',
    //        data: $scope.provisionaleducational,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.showProvisionalEducationDetailFlag = false;
    //                $scope.getProvisionalMeritListEducationDetailsGetList($scope.selectedProvisional);
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // delete  Provisional Meritlist Educational details
    $scope.deleteProvisionalMeritListEducationDetails = function (data) {
        $scope.manualParametersFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.deleteManualParametersStatic(index);
        /*   }*/
        if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteProvisionalMeritListEducationDetailsDynamic(data);
        }
    }

    //$scope.deleteManualParametersStatic = function (index) {

    //    var confirm5 = $mdDialog.confirm()
    //        .title('Would You Delete Manual Parameter ?')
    //        .ok('Please do it!')
    //        .cancel('No, Just Checking!');
    //    $mdDialog.show(confirm5).then(function () {
    //        $scope.meritListManualParameterConfigurationGetList.splice(index, 1);
    //    }, function () {
    //    });
    //    $scope.manualParametersFlag = false;
    //}

    $scope.deleteProvisionalMeritListEducationDetailsDynamic = function (data) {

        var confirm6 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Provisional Meritlist  Educational Details ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm6).then(function () {

            $http({
                method: 'POST',
                url: 'api/ProvisionalMeritListEducationDetails/ProvisionalMeritListEducationDetailsDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getMeritListManualParameterConfigurationGetList($scope.selectedProvisional);
                        $scope.showProvisionalEducationDetailFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }, function () {

        });
    };

    /*   Provisional Meritlist Add On Information*/
    // for get Provisional Meritlist Add On Information
    $scope.getProvisionalMeritListAddOnInformationGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.ProvisionalMeritListId  = data.Id;
  

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListAddOnInformation/ProvisionalMeritListAddOnInformationGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.provisionalMeritListAddOnInformationGetList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showProvisionalAddOnInformationFlag = false;
    $scope.editProvisionalMeritlistAddOnInformation = function (data) {
        data.showProvisionalAddOnInformationFlag = true;
     
    }

    // for save  Provisional Meritlist Add On Information with condition
    $scope.saveProvisionalMeritlistAddOnInformation = function (data) {
        $scope.showProvisionalAddOnInformationFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.saveProvisionalMeritListAddOnInformationAdd(data);
        //}
        if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editProvisionalMeritListAddOnInformationEdit(data);
        }

    }

    // for edit  Provisional Meritlist Add On Information
    $scope.editProvisionalMeritListAddOnInformationEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.showProvisionalAddOnInformationFlag = true;
        $scope.provisionaladdoninformation = data;

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListAddOnInformation/ProvisionalMeritListAddOnInformationEdit',
            data: $scope.provisionaladdoninformation,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.showProvisionalAddOnInformationFlag = true;
                    $scope.getProvisionalMeritListAddOnInformationGetList($scope.selectedProvisional);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Provisional Meritlist Add On Information
    //$scope.saveProvisionalMeritListAddOnInformationAdd = function (data) {

    //    $rootScope.showLoading = true;
    //    $scope.showProvisionalAddOnInformationFlag = true;
    //    $scope.provisionaladdoninformation = data;
    //    /*    $scope.provisionaleducational.MeritListInstanceId = $scope.MeritListInstanceId3;*/

    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalMeritListAddOnInformation/ProvisionalMeritListAddOnInformationAdd',
    //        data: $scope.provisionaladdoninformation,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.showProvisionalAddOnInformationFlag = false;
    //                $scope.getProvisionalMeritListAddOnInformationGetList($scope.selectedProvisional);
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // delete  Provisional Meritlist Add On Information
    $scope.deleteProvisionalMeritlistAddOnInformation = function (data) {
        $scope.showProvisionalAddOnInformationFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.deleteManualParametersStatic(index);
        /*   }*/
        if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteProvisionalMeritlistAddOnInformationDynamic(data);
        }
    }

    //$scope.deleteManualParametersStatic = function (index) {

    //    var confirm5 = $mdDialog.confirm()
    //        .title('Would You Delete Manual Parameter ?')
    //        .ok('Please do it!')
    //        .cancel('No, Just Checking!');
    //    $mdDialog.show(confirm5).then(function () {
    //        $scope.meritListManualParameterConfigurationGetList.splice(index, 1);
    //    }, function () {
    //    });
    //    $scope.manualParametersFlag = false;
    //}

    $scope.deleteProvisionalMeritlistAddOnInformationDynamic = function (data) {

        var confirm6 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Provisional Meritlist Add On Information ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm6).then(function () {

            $http({
                method: 'POST',
                url: 'api/ProvisionalMeritListAddOnInformation/ProvisionalMeritListAddOnInformationDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getProvisionalMeritListAddOnInformationGetList($scope.selectedProvisional);
                        $scope.showProvisionalAddOnInformationFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }, function () {

        });
    };

    /*   Provisional Meritlist Manual Parameter Values*/
    // for get Provisional Meritlist Manual Parameter Values
    $scope.getProvisionalMeritListManualParameterValuesGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.ProvisionalMeritListId  = data.Id;
        //$scope.MeritListInstanceId3 = data.Id;
        //$scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListManualParameterValues/ProvisionalMeritListManualParameterValuesGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.provisionalMeritListManualParameterValuesGetList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showProvisionalManualParameterFlag = false;
    $scope.editProvisionalMeritlistManualParameterValues = function (data) {
        data.showProvisionalManualParameterFlag = true;
    }

    // for save  Provisional Meritlist Manual Parameter Values with condition
    $scope.saveProvisionalMeritlistManualParameterValues = function (data) {
        $scope.showProvisionalManualParameterFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.saveProvisionalMeritListManualParameterValuesAdd(data);
        //}
         if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editProvisionalMeritListManualParameterValuesEdit(data);
        }

    }

    // for edit  Provisional Meritlist Manual Parameter Values
    $scope.editProvisionalMeritListManualParameterValuesEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.showProvisionalManualParameterFlag = true;
        $scope.provisionalmanualparameter = data;


        $http({
            method: 'POST',
            url: 'api/ProvisionalMeritListManualParameterValues/ProvisionalMeritListManualParameterValuesEdit',
            data: $scope.provisionalmanualparameter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.showProvisionalAddOnInformationFlag = true;
                    $scope.getProvisionalMeritListManualParameterValuesGetList($scope.selectedProvisional);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Provisional Meritlist Manual Parameter Values
    //$scope.saveProvisionalMeritListManualParameterValuesAdd = function (data) {

    //    $rootScope.showLoading = true;
    //    $scope.showProvisionalManualParameterFlag = true;
    //    $scope.provisionalmanualparameter = data;
    //    /*    $scope.provisionaleducational.MeritListInstanceId = $scope.MeritListInstanceId3;*/

    //    $http({
    //        method: 'POST',
    //        url: 'api/ProvisionalMeritListManualParameterValues/ProvisionalMeritListManualParameterValuesAdd',
    //        data: $scope.provisionalmanualparameter,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.showProvisionalManualParameterFlag = false;
    //                $scope.getProvisionalMeritListManualParameterValuesGetList($scope.selectedProvisional);
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // delete  Provisional Meritlist Manual Parameter Values
    $scope.deleteProvisionalMeritlistManualParameterValues = function (data) {
        $scope.showProvisionalAddOnInformationFlag = true;

        //if (data.Id === undefined || data.Id === null || data.Id === '') {
        //    $scope.deleteManualParametersStatic(index);
        /*   }*/
        if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteProvisionalMeritlistManualParameterValuesDynamic(data);
        }
    }

    //$scope.deleteManualParametersStatic = function (index) {

    //    var confirm5 = $mdDialog.confirm()
    //        .title('Would You Delete Manual Parameter ?')
    //        .ok('Please do it!')
    //        .cancel('No, Just Checking!');
    //    $mdDialog.show(confirm5).then(function () {
    //        $scope.meritListManualParameterConfigurationGetList.splice(index, 1);
    //    }, function () {
    //    });
    //    $scope.manualParametersFlag = false;
    //}

    $scope.deleteProvisionalMeritlistManualParameterValuesDynamic = function (data) {

        var confirm6 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Provisional Meritlist Manual Parameter Values ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm6).then(function () {

            $http({
                method: 'POST',
                url: 'api/ProvisionalMeritListManualParameterValues/ProvisionalMeritListManualParameterValuesDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getProvisionalMeritListManualParameterValuesGetList($scope.selectedProvisional);
                        $scope.showProvisionalManualParameterFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }, function () {

        });
    };


    // for tab 3
    // for Merit List Special Category Configuration
    $scope.getMeritlistSpecialCategoryConfigurationGetList = function (data) {

        $rootScope.showLoading = true;
        var xml = new Object();
        xml.MeritListInstanceId = data.Id;
        $scope.MeritListInstanceId15 = data.Id;
        $scope.selectedMeritInstance = data;

        $http({
            method: 'POST',
            url: 'api/MeritListSpecialCategoryConfiguration/MeritListSpecialCategoryConfigurationGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.meritlistSpecialCategoryConfigurationGetList = response.obj;

                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit Merit List Special Category Configuration with condition
    $scope.meritListSpecialCategotyConfigurationFlag = false;
    $scope.editMeritListSpecialCategoryConfiguration = function (data) {
        data.meritListSpecialCategotyConfigurationFlag = true;
   
    }

    // for save Merit List Special Category Configuration with condition
    $scope.saveMeritListSpecialCategoryConfiguration = function (data) {
        $scope.meritListSpecialCategotyConfigurationFlag = true;

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveMeritListSpecialCategoryConfigurationAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editMeritListSpecialCategoryConfigurationEdit(data);
        }
    }

    // for edit Merit List Special Category Configuration
    $scope.editMeritListSpecialCategoryConfigurationEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.meritListSpecialCategotyConfigurationFlag = true;
        $scope.meritlistspecialcategotyconfiguration = data;
        data.MeritListInstanceId = $scope.MeritListInstanceId15;


        $http({
            method: 'POST',
            url: 'api/MeritListSpecialCategoryConfiguration/MeritListSpecialCategoryConfigurationEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    alert(response.obj);
                }
                else {

                    alert(response.obj);

                    $scope.getMeritlistSpecialCategoryConfigurationGetList($scope.selectedMeritInstance);
                    $scope.meritListSpecialCategotyConfigurationFlag = false;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save Merit List Special Category Configuration
    $scope.saveMeritListSpecialCategoryConfigurationAdd = function (data) {

        $rootScope.showLoading = true;
        $scope.meritListSpecialCategotyConfigurationFlag = true;
        $scope.meritlistspecialcategotyconfiguration = data;
        $scope.meritlistspecialcategotyconfiguration.MeritListInstanceId = $scope.MeritListInstanceId15;

        $http({
            method: 'POST',
            url: 'api/MeritListSpecialCategoryConfiguration/MeritListSpecialCategoryConfigurationAdd',
            data: $scope.meritlistspecialcategotyconfiguration,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);

                    $scope.getMeritlistSpecialCategoryConfigurationGetList($scope.selectedMeritInstance);
                    $scope.meritListSpecialCategotyConfigurationFlag = true;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // delete Merit List Special Category Configuration
    $scope.deleteMeritListSpecialCategoryConfiguration = function (data, index) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.deleteMeritListSpecialCategoryConfigurationStatic(index);
        } else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.deleteMeritListSpecialCategoryConfigurationDynamic(data);
        }
    }


    $scope.deleteMeritListSpecialCategoryConfigurationStatic = function (index) {

        var confirm15 = $mdDialog.confirm()
            .title('Would You Delete Merit List Special Category Configuration ?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');
        $mdDialog.show(confirm15).then(function () {
            $scope.meritlistSpecialCategoryConfigurationGetList.splice(index, 1);
        }, function () {
        });
        $scope.meritListSpecialCategotyConfigurationFlag = false;
    }

    $scope.deleteMeritListSpecialCategoryConfigurationDynamic = function (data) {
    
        var confirm16 = $mdDialog.confirm()
            .title('Are You Sure Want To Delete Merit List Special Category Configuration?')
            .ok('Please do it!')
            .cancel('No, Just Checking!');

        $mdDialog.show(confirm16).then(function () {

            $http({
                method: 'POST',
                url: 'api/MeritListSpecialCategoryConfiguration/MeritListSpecialCategoryConfigurationDelete',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getMeritlistSpecialCategoryConfigurationGetList($scope.selectedMeritInstance);
                        $scope.meritListSpecialCategotyConfigurationFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


        }, function () {

        });
    };

    // for add Merit List Special Category Configuration
    $scope.addMeritListSpecialCategoryConfigurationToInstance = function (index) {
        var meritlistspecialcategotyconfiguration = {
            meritListSpecialCategotyConfigurationFlag: true,
            GenReservationCategoryId: "",
            EWSReservedPercentage: "",
            PhysicallyHandicapReservedPercentage: "",
    
        };

        $scope.meritlistSpecialCategoryConfigurationGetList.push(meritlistspecialcategotyconfiguration);
    };


    // for blank array of Merit List Special Category Configuration
    $scope.meritlistSpecialCategoryConfigurationGetList = [
        {
            GenReservationCategoryId: "",
            EWSReservedPercentage: "",
            PhysicallyHandicapReservedPercentage: "",
        }];

});