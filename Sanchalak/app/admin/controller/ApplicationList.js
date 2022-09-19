app.controller('ApplicationListCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    
	//Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }	
    $scope.AppList = {};
   
    $scope.TakeLocalStoradeValue = function () {
        
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.AppList.InstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.ApplicationListSearchByAppStatusInComp();
        }
        else {
            $localStorage.Stats = null;
            $scope.AppList = null;
        }
    };
	$scope.GetLocalStorageValue = function () {        
        
        if ($localStorage.Stats.FlagFromAppStats == true) {

            $scope.AppList.InstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.ApplicationListSearch();        
        }                        
    };

   
    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {

        $http({
            method: 'Post',
            url: 'api/ApplicationList/IncProgInsPartTermListGetByInstituteId',
            data: { InstituteId: $localStorage.Stats.InstituteId},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.alGet = response.obj;
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetPartTermList = function () {
        
        if ($localStorage.Stats.InstituteId == null || $localStorage.Stats.InstituteId == undefined)
        { $scope.ApplicationListGet(); }
        else
        { $scope.getIncProgInsPartTermListByInstituteId();}
    };

    // This method is for getting InstancePartTerm
    $scope.ApplicationListGet = function () {

        $http({
            method: 'GET',
            url: 'api/ApplicationList/ApplicationListGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                
                $scope.alGet = response.obj;                
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };  

    // This method is for searching short details of applicant according to ProgrammeInstancePartTermId
    $scope.ApplicationListSearch = function () {
        
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.AppList.InstancePartTermId == "" ||
            $scope.AppList.InstancePartTermId == null ||
            $scope.AppList.InstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }				
        $http({
            method: 'POST',
            url: 'api/ApplicationList/ApplicationListSearch',
            data: $scope.AppList,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

				alert("Please wait, Data is processing...");
				$scope.onSpinner();
		
                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.ApplicationListTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });
                        $scope.searchCaseResult = undefined;
                        $scope.searchCaseResultFull = undefined;
						$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);  
						$scope.offSpinner();						
                }
                }
                else {
                    $scope.ApplicationListTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });                    
                    $scope.searchCaseResult = response.obj;                    
                    $scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    // This method is for searching full details of applicant according to ProgrammeInstancePartTermId
    $scope.ApplicationListSearchFull = function () {

        //alert($scope.AppList.InstancePartTermId);

        $http({
            method: 'POST',
            url: 'api/ApplicationList/ApplicationListSearchFull',
            data: $scope.AppList,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {

                    alert(response.obj);
					$scope.offSpinner();
                }
                else {
                    
                    $scope.searchCaseResultFull = response.obj;
					$scope.offSpinner();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    // This method is for exporting data in excel format with short details
    $scope.exportData = function () {

        if ($scope.searchCaseResult == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ConfirmApplicationListWithShortDetails_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Confirm Application List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmApplicationIdStr', title: 'Application Id' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Name', title: 'Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'IsLocalToVadodara', title: 'Is LocalToVadodara?' },
                { columnid: 'SocialCategoryName', title: 'Special Quota' },
                { columnid: 'ReservationCategory', title: 'Reservation Category' },
                { columnid: 'IsEWS', title: 'Is EWS?' },
                { columnid: 'IsPhysicallyChallenged', title: 'Is PhysicallyChallenged?' },
                { columnid: 'DisabilityType', title: 'Disability Type' },
                { columnid: 'DisabilityPercentage', title: 'Disability Percentage' },
                { columnid: 'IsAdmitted', title: 'Is Admitted?' },
                { columnid: 'AdmittedOn', title: 'Admitted On' },
                { columnid: 'AdmittedInstituteName', title: 'Admitted Institute' },
                { columnid: 'AdminRemarkByFaculty', title: 'Admin Remark By Faculty' },
                { columnid: 'AdminRemarkByAcademics', title: 'Admin Remark By Academics' },
                { columnid: 'VerifiedStatus', title: 'Verified Status' },
                { columnid: 'IsCancelledByStudent', title: 'Cancelled Application Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.searchCaseResult]);
    };
    // This method is for exporting data in excel format with full details
    $scope.exportDataFull = function () {

        if ($scope.searchCaseResultFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ConfirmApplicationListWithFullDetails_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Confirm Application List | Date and Time: ' + DateAndTime,                
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmApplicationIdStr', title: 'Application Id' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Name', title: 'Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'AdmApplicantRegistrationId', title: 'Applicant Registration Id' },
                { columnid: 'UserName', title: 'User Name' },
                { columnid: 'NameAsPerMarksheet', title: 'Name As Per Marksheet' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'DOB', title: 'DOB' },
                { columnid: 'DOBDoc', title: 'DOB Document' },
                { columnid: 'PhotoIdDoc', title: 'PhotoId Document' },
                { columnid: 'HeightInCms', title: 'Height In Cms' },
                { columnid: 'WeightInKgs', title: 'Weight In Kgs' },
                { columnid: 'NameOfFather', title: 'Father Name' },
                { columnid: 'NameOfMother', title: 'Mother Name' },
                { columnid: 'SpouseName', title: 'Spouse Name' },
                { columnid: 'MaritalStatus', title: 'Marital Status' },
                { columnid: 'FatherMotherContactNo', title: 'Parents Contact No' },
                { columnid: 'FatherOccupation', title: 'Father Occupation' },
                { columnid: 'MotherOccupation', title: 'Mother Occupation' },
                { columnid: 'FamilyAnnualIncome', title: 'Family Annual Income' },
                { columnid: 'GuardianName', title: 'Guardian Name' },
                { columnid: 'GuardianContactNo', title: 'Guardian ContactNo' },
                { columnid: 'GuardianOccupation', title: 'Guardian Occupation' },
                { columnid: 'GuardianAnnualIncome', title: 'Guardian Annual Income' },
                { columnid: 'IsGuardianEbc', title: 'Is Guardian Ebc?' },
                { columnid: 'GSocialCategory', title: 'Guardian Social Category' },
                { columnid: 'MotherTongueName', title: 'Mother Tongue' },
                { columnid: 'BloodGroupName', title: 'Blood Group' },
                { columnid: 'IsMajorThelesamiaStatus', title: 'Thalassemia Status' },
                { columnid: 'ReligionName', title: 'Religion' },
                { columnid: 'OtherReligion', title: 'Other Religion' },
                { columnid: 'PassportNumber', title: 'Passport Number' },
                { columnid: 'PassportDate', title: 'Passport Date' },
                { columnid: 'AadharNumber', title: 'Aadhar Number' },
                { columnid: 'AadharDoc', title: 'Aadhar Document' },
                { columnid: 'NameOnAadhar', title: 'Name as per Aadhar' },
                { columnid: 'IsEmp', title: 'Is SelfEmployee?' },
                { columnid: 'CurrentEmployerName', title: 'Current Employer Name' },
                { columnid: 'IsSmsPermissionGiven', title: 'Is SMS Permission Given?' },
                { columnid: 'IsLocalToVadodara', title: 'Is LocalToVadodara?' },
                { columnid: 'IsNRI', title: 'Is NRI?' },
                { columnid: 'CitizenCountry', title: 'Citizen Country' },
                { columnid: 'PermanentAddress', title: 'Permanent Address' },
                { columnid: 'PermanentCountry', title: 'Permanent Country' },
                { columnid: 'PermanentState', title: 'Permanent State' },
                { columnid: 'PermanentDistrict', title: 'Permanent District' },
                { columnid: 'PermanentCityVillage', title: 'Permanent City/ Village' },
                { columnid: 'PermanentPincode', title: 'Permanent Pincode' },
                { columnid: 'IsCurrentAsPermanent', title: 'Is Current Add. Same as Permanent Add.?' },
                { columnid: 'CurrentAddress', title: 'Current Address' },
                { columnid: 'CurrentCountry', title: 'Current Country' },
                { columnid: 'CurrentState', title: 'Current State' },
                { columnid: 'CurrentDistrict', title: 'Current District' },
                { columnid: 'CurrentCityVillage', title: 'Current City/ Village' },
                { columnid: 'CurrentPincode', title: 'Current Pincode' },
                { columnid: 'OptionalMobileNo', title: 'Alternate Phone No.' },
                { columnid: 'SocialCategoryName', title: 'Special Quota' },
                { columnid: 'SocialCategoryDoc', title: 'Special Quota Document' },
                { columnid: 'IsSocialDocSubmitted', title: 'Special Quota Doc Declaration' },
                { columnid: 'ReservationCategory', title: 'Reservation Category' },
                { columnid: 'ReservationCategoryDoc', title: 'Reservation Category Doc' },
                { columnid: 'IsReservationDocSubmitted', title: 'Reservation Doc Declaration' },
                { columnid: 'IsEWS', title: 'Is EWS?' },
                { columnid: 'EWSDoc', title: 'EWS Document' },
                { columnid: 'IsEWSDocSubmitted', title: 'EWS Doc Declaration' },
                { columnid: 'IsPhysicallyChallenged', title: 'Is Disability Challenged?' },
                { columnid: 'PCDoc', title: 'Disability Challenged Document' },
                { columnid: 'IsPCDocSubmitted', title: 'Disability Challenged Doc Declaration' },
                { columnid: 'DisabilityType', title: 'Disability Type' },
                { columnid: 'DisabilityPercentage', title: 'Disability Percentage' },
                { columnid: 'ApplicantPhoto', title: 'Applicant Photo' },
                { columnid: 'ApplicantSignature', title: 'Applicant Signature' },
                { columnid: 'EligibleDegreeName', title: 'Eligible Degree' },
                { columnid: 'SpecializationName', title: 'Specialization' },
                { columnid: 'ExaminationBodyName', title: 'Examination Body' },
                { columnid: 'InstituteAttended', title: 'Faculty/ College/ School Name' },
                { columnid: 'SchoolNo', title: 'School No' },
                { columnid: 'CityName', title: 'City' },
                { columnid: 'OtherCity', title: 'Other City' },
                { columnid: 'ExamPassCity', title: 'Exam Pass Location' },
                { columnid: 'ExamPassMonth', title: 'Exam Pass Month' },
                { columnid: 'ExamPassYear', title: 'Exam Pass Year' },
                { columnid: 'ExamSeatNumber', title: 'Exam Seat Number' },
                { columnid: 'ExamCertificateNumber', title: 'Exam Certificate Number' },
                { columnid: 'MarkObtained', title: 'Mark Obtained' },
                { columnid: 'MarkOutof', title: 'Mark Outof' },
                { columnid: 'Grade', title: 'Grade' },
                { columnid: 'CGPA', title: 'CGPA' },
                { columnid: 'PercentageEquivalenceCGPA', title: 'Percentage Equivalence CGPA' },
                { columnid: 'Percentage', title: 'Percentage' },
                { columnid: 'ClassName', title: 'Class' },
                { columnid: 'IsFirstTrial', title: 'Is First Trial?' },
                { columnid: 'IsLastQualifyingExam', title: 'Is Last Qualifying Exam?' },
                { columnid: 'LanguageName', title: 'Medium Language' },
                { columnid: 'ResultStatus', title: 'Result Status' },
                { columnid: 'AttachDocument', title: 'Attach Document' },
                { columnid: 'IsDeclared', title: 'Doc Declaration' },
                { columnid: 'IsAdmitted', title: 'Is Admitted?' },
                { columnid: 'AdmittedOn', title: 'Admitted On' },
                { columnid: 'AdmittedInstituteName', title: 'Admitted Institute' },
                { columnid: 'AdminRemarkByFaculty', title: 'Admin Remark By Faculty' },
                { columnid: 'AdminRemarkByAcademics', title: 'Admin Remark By Academics' },
                { columnid: 'VerifiedStatus', title: 'Verified Status' },
                { columnid: 'IsCancelledByStudent', title: 'Cancelled Application Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.searchCaseResultFull]);
    };

    //------Archana's Code Start------ Created on 29-June-2021

    // This method is for searching short details of applicant according to ProgrammeInstancePartTermId
    $scope.ApplicationListSearchByAppStatusInComp = function () {
       
        //var InsPartTerm = $('#InsPartTerm').val();
        //if (InsPartTerm == null || InsPartTerm == "") {

        //    alert("Please select Programme Instance Part Term");
        //    return false;
        //}
        if ($scope.AppList.InstancePartTermId == null || $scope.AppList.InstancePartTermId === undefined || $scope.AppList.InstancePartTermId ==""
          ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ApplicationList')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Programme Instance Part Term before Search...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/ApplicationList/ApplicationListSearchByAppStatusInComp',
                data: $scope.AppList,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                       // alert('Record Not Exists');
                        $scope.tableShow = false;
                    }
                    else {
                        $scope.tableShow = true;
                        alert("Please wait, Data is processing...");
                        $scope.ApplicationListByStatusInCompTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj

                        });
                        $scope.AppStatusInCompShort = response.obj;
                        $scope.ApplicationListSearchFullByAppStatusInComp();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };

    $scope.backToList = function () {
        //debugger;
        //alert($localStorage.InstId);
        if ($localStorage.InstId == null || $localStorage.InstId == undefined) {
            $state.go('ApplicationStatistics');

        }
        else {


            $rootScope.Checkls = true;
            $state.go('ApplicationStatisticsByInstitute');
        }



    };
   
    // This method is for searching full details of applicant according to ProgrammeInstancePartTermId
    $scope.ApplicationListSearchFullByAppStatusInComp = function () {
     
        $http({
            method: 'POST',
            url: 'api/ApplicationList/ApplicationListSearchFullByAppStatusInComp',
            data: $scope.AppList,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ApplicationListByStatusInCompTableParamsFull = new NgTableParams({
                        //page: 1,
                        //count: response.obj.length
                    }, {
                        dataset: response.obj
                    });
                    $scope.AppStatusInCompFull = response.obj;
                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    // This method is for exporting data in excel format with short details
    $scope.exportDataAppStatusInComp = function () {
        if ($scope.AppStatusInCompShort  == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationListWhoseAppFeeNotPaidWithShortDetails_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Application List Whose Application Fee Not Paid Short Details | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmApplicationIdStr', title: 'Application Id' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Name', title: 'Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'IsLocalToVadodara', title: 'Is LocalToVadodara?' },
                { columnid: 'SocialCategoryName', title: 'Special Quota' },
                { columnid: 'ReservationCategory', title: 'Reservation Category' },
                { columnid: 'IsEWS', title: 'Is EWS?' },
                { columnid: 'IsPhysicallyChallenged', title: 'Is PhysicallyChallenged?' },
                { columnid: 'DisabilityType', title: 'Disability Type' },
                { columnid: 'DisabilityPercentage', title: 'Disability Percentage' },
                { columnid: 'IsAdmitted', title: 'Is Admitted?' },
                { columnid: 'AdmittedOn', title: 'Admitted On' },
                { columnid: 'AdmittedInstituteName', title: 'Admitted Institute' },
                { columnid: 'AdminRemarkByFaculty', title: 'Admin Remark By Faculty' },
                { columnid: 'AdminRemarkByAcademics', title: 'Admin Remark By Academics' },
                { columnid: 'VerifiedStatus', title: 'Verified Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.AppStatusInCompShort]);
    };
    // This method is for exporting data in excel format with full details
    $scope.exportDataFullAppStatusInComp = function () {
        if ($scope.AppStatusInCompFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationListWhoseAppFeeNotPaidWithFullDetails_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Application List Whose Application Fee Not Paid Full Details | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmApplicationIdStr', title: 'Application Id' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Name', title: 'Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'AdmApplicantRegistrationId', title: 'Applicant Registration Id' },
                { columnid: 'UserName', title: 'User Name' },
                { columnid: 'NameAsPerMarksheet', title: 'Name As Per Marksheet' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'DOB', title: 'DOB' },
                { columnid: 'DOBDoc', title: 'DOB Document' },
                { columnid: 'PhotoIdDoc', title: 'PhotoId Document' },
                { columnid: 'HeightInCms', title: 'Height In Cms' },
                { columnid: 'WeightInKgs', title: 'Weight In Kgs' },
                { columnid: 'NameOfFather', title: 'Father Name' },
                { columnid: 'NameOfMother', title: 'Mother Name' },
                { columnid: 'SpouseName', title: 'Spouse Name' },
                { columnid: 'MaritalStatus', title: 'Marital Status' },
                { columnid: 'FatherMotherContactNo', title: 'Parents Contact No' },
                { columnid: 'FatherOccupation', title: 'Father Occupation' },
                { columnid: 'MotherOccupation', title: 'Mother Occupation' },
                { columnid: 'FamilyAnnualIncome', title: 'Family Annual Income' },
                { columnid: 'GuardianName', title: 'Guardian Name' },
                { columnid: 'GuardianContactNo', title: 'Guardian ContactNo' },
                { columnid: 'GuardianOccupation', title: 'Guardian Occupation' },
                { columnid: 'GuardianAnnualIncome', title: 'Guardian Annual Income' },
                { columnid: 'IsGuardianEbc', title: 'Is Guardian Ebc?' },
                { columnid: 'GSocialCategory', title: 'Guardian Social Category' },
                { columnid: 'MotherTongueName', title: 'Mother Tongue' },
                { columnid: 'BloodGroupName', title: 'Blood Group' },
                { columnid: 'IsMajorThelesamiaStatus', title: 'Thalassemia Status' },
                { columnid: 'ReligionName', title: 'Religion' },
                { columnid: 'OtherReligion', title: 'Other Religion' },
                { columnid: 'PassportNumber', title: 'Passport Number' },
                { columnid: 'PassportDate', title: 'Passport Date' },
                { columnid: 'AadharNumber', title: 'Aadhar Number' },
                { columnid: 'AadharDoc', title: 'Aadhar Document' },
                { columnid: 'NameOnAadhar', title: 'Name as per Aadhar' },
                { columnid: 'IsEmp', title: 'Is SelfEmployee?' },
                { columnid: 'CurrentEmployerName', title: 'Current Employer Name' },
                { columnid: 'IsSmsPermissionGiven', title: 'Is SMS Permission Given?' },
                { columnid: 'IsLocalToVadodara', title: 'Is LocalToVadodara?' },
                { columnid: 'IsNRI', title: 'Is NRI?' },
                { columnid: 'CitizenCountry', title: 'Citizen Country' },
                { columnid: 'PermanentAddress', title: 'Permanent Address' },
                { columnid: 'PermanentCountry', title: 'Permanent Country' },
                { columnid: 'PermanentState', title: 'Permanent State' },
                { columnid: 'PermanentDistrict', title: 'Permanent District' },
                { columnid: 'PermanentCityVillage', title: 'Permanent City/ Village' },
                { columnid: 'PermanentPincode', title: 'Permanent Pincode' },
                { columnid: 'IsCurrentAsPermanent', title: 'Is Current Add. Same as Permanent Add.?' },
                { columnid: 'CurrentAddress', title: 'Current Address' },
                { columnid: 'CurrentCountry', title: 'Current Country' },
                { columnid: 'CurrentState', title: 'Current State' },
                { columnid: 'CurrentDistrict', title: 'Current District' },
                { columnid: 'CurrentCityVillage', title: 'Current City/ Village' },
                { columnid: 'CurrentPincode', title: 'Current Pincode' },
                { columnid: 'OptionalMobileNo', title: 'Alternate Phone No.' },
                { columnid: 'SocialCategoryName', title: 'Special Quota' },
                { columnid: 'SocialCategoryDoc', title: 'Special Quota Document' },
                { columnid: 'IsSocialDocSubmitted', title: 'Special Quota Doc Declaration' },
                { columnid: 'ReservationCategory', title: 'Reservation Category' },
                { columnid: 'ReservationCategoryDoc', title: 'Reservation Category Doc' },
                { columnid: 'IsReservationDocSubmitted', title: 'Reservation Doc Declaration' },
                { columnid: 'IsEWS', title: 'Is EWS?' },
                { columnid: 'EWSDoc', title: 'EWS Document' },
                { columnid: 'IsEWSDocSubmitted', title: 'EWS Doc Declaration' },
                { columnid: 'IsPhysicallyChallenged', title: 'Is Disability Challenged?' },
                { columnid: 'PCDoc', title: 'Disability Challenged Document' },
                { columnid: 'IsPCDocSubmitted', title: 'Disability Challenged Doc Declaration' },
                { columnid: 'DisabilityType', title: 'Disability Type' },
                { columnid: 'DisabilityPercentage', title: 'Disability Percentage' },
                { columnid: 'ApplicantPhoto', title: 'Applicant Photo' },
                { columnid: 'ApplicantSignature', title: 'Applicant Signature' },
                { columnid: 'EligibleDegreeName', title: 'Eligible Degree' },
                { columnid: 'SpecializationName', title: 'Specialization' },
                { columnid: 'ExaminationBodyName', title: 'Examination Body' },
                { columnid: 'InstituteAttended', title: 'Faculty/ College/ School Name' },
                { columnid: 'SchoolNo', title: 'School No' },
                { columnid: 'CityName', title: 'City' },
                { columnid: 'OtherCity', title: 'Other City' },
                { columnid: 'ExamPassCity', title: 'Exam Pass Location' },
                { columnid: 'ExamPassMonth', title: 'Exam Pass Month' },
                { columnid: 'ExamPassYear', title: 'Exam Pass Year' },
                { columnid: 'ExamSeatNumber', title: 'Exam Seat Number' },
                { columnid: 'ExamCertificateNumber', title: 'Exam Certificate Number' },
                { columnid: 'MarkObtained', title: 'Mark Obtained' },
                { columnid: 'MarkOutof', title: 'Mark Outof' },
                { columnid: 'Grade', title: 'Grade' },
                { columnid: 'CGPA', title: 'CGPA' },
                { columnid: 'PercentageEquivalenceCGPA', title: 'Percentage Equivalence CGPA' },
                { columnid: 'Percentage', title: 'Percentage' },
                { columnid: 'ClassName', title: 'Class' },
                { columnid: 'IsFirstTrial', title: 'Is First Trial?' },
                { columnid: 'IsLastQualifyingExam', title: 'Is Last Qualifying Exam?' },
                { columnid: 'LanguageName', title: 'Medium Language' },
                { columnid: 'ResultStatus', title: 'Result Status' },
                { columnid: 'AttachDocument', title: 'Attach Document' },
                { columnid: 'IsDeclared', title: 'Doc Declaration' },
                { columnid: 'IsAdmitted', title: 'Is Admitted?' },
                { columnid: 'AdmittedOn', title: 'Admitted On' },
                { columnid: 'AdmittedInstituteName', title: 'Admitted Institute' },
                { columnid: 'AdminRemarkByFaculty', title: 'Admin Remark By Faculty' },
                { columnid: 'AdminRemarkByAcademics', title: 'Admin Remark By Academics' },
                { columnid: 'VerifiedStatus', title: 'Verified Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.AppStatusInCompFull]);
    };
    //------Archana's Code End------

    $scope.backToList = function () {
        //debugger;
        //alert($localStorage.InstId);
        if ($localStorage.InstId == null || $localStorage.InstId == undefined) {
            $state.go('ApplicationStatistics');

        }
        else {


            $rootScope.Checkls = true;
            $state.go('ApplicationStatisticsByInstitute');
        }



    };

});



