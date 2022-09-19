app.controller('PrnGeneratedCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.PrnGenerated = {}

    $scope.TakeLocalStoradeValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.PrnGenerated.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getPrnGenerated();
        }
        else {
            $localStorage.Stats = null;
            $scope.PrnGenerated = null;
        }
    };


    $scope.GetLocalStorageValue = function () {

        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.PrnGenerated.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.InstId = $localStorage.Stats.InstituteId;
            $scope.getPrnGenerated();
        }
        else {
            $localStorage.Stats = null;
            $scope.PrnGenerated = null;
        }
    };

    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {

        $http({
            method: 'Post',
            url: 'api/PrnGenerated/IncProgInsPartTermListGetByInstituteId',
            data: { InstituteId: $localStorage.Stats.InstituteId },
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

        if ($localStorage.Stats.InstituteId == null || $localStorage.Stats.InstituteId == undefined) { $scope.ApplicationListGet(); }
        else { $scope.getIncProgInsPartTermListByInstituteId(); }
    };


    // This method is for getting InstancePartTerm
    $scope.ApplicationListGet = function () {
        //alert("Institute");
        $http({
            method: 'GET',
            url: 'api/PrnGenerated/ApplicationListGet',
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


    //Get Students Admission Fees Paid Report
    $scope.getPrnGenerated = function () {

        // alert($scope.StudentAdmissionFeesPaidReport.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.PrnGenerated.ProgrammeInstancePartTermId == "" ||
            $scope.PrnGenerated.ProgrammeInstancePartTermId == null ||
            $scope.PrnGenerated.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/PrnGenerated/PRNGeneratedGet',
            data: { ProgrammeInstancePartTermId: $scope.PrnGenerated.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                alert("Please wait, Data is processing...");
                //$scope.onSpinner();

                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.PrnGeneratedTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });

                        $scope.exportDataShort = undefined;
                        $scope.exportDataFull = undefined;
                        //$scope.searchCaseResultFull = undefined;
                        //$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.PrnGeneratedTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataShort = response.obj;
                    $scope.exportDataFull = response.obj;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Excel Students Admission Fees Paid Report
    $scope.PRNExportData = function () {
        //debugger
        if ($scope.exportDataShort == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PRNGenerationReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'PRN Generation Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmissionId', title: 'AdmissionId' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Name' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'EligibilityByAcademics', title: 'Academic Status' },
                { columnid: 'AdminRemarkByAcademics', title: 'Academic Remarks' },
                { columnid: 'ApplicationReservationName', title: 'Programme Name' },
                { columnid: 'SocialCategoryName', title: 'Branch Name' },
                { columnid: 'CommitteeName', title: 'Institute Name' },
                { columnid: 'IsPhysicallyChanllenged', title: 'IsPhysicallyChanllenged' },
                { columnid: 'Nationality', title: 'Nationality' },
                { columnid: 'CurrentAddress', title: 'Correspondence Address' },
                { columnid: 'PermanentAddress', title: 'Permanent Address' },
                { columnid: 'FatherMotherContactNo', title: 'Parents Mobile No.' },
                { columnid: 'LocalToVadodara', title: 'Local To Vadodara' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'FeeCategoryName', title: 'IsFee Category Name' },
                { columnid: 'PRNGenerated', title: 'PRN Generated' },



            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataShort]);
    };


    $scope.PRNExportFull = function () {
        debugger
        if ($scope.exportDataFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PRNGenerationReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'PRN Generation Full Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AdmissionId', title: 'AdmissionId' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Name' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'Gender', title: 'Gender' },
                { columnid: 'EligibilityByAcademics', title: 'Academic Status' },
                { columnid: 'AdminRemarkByAcademics', title: 'Academic Remarks' },
                { columnid: 'ApplicationReservationName', title: 'Programme Name' },
                { columnid: 'SocialCategoryName', title: 'Branch Name' },
                { columnid: 'CommitteeName', title: 'Institute Name' },
                { columnid: 'IsPhysicallyChanllenged', title: 'IsPhysicallyChanllenged' },
                { columnid: 'Nationality', title: 'Nationality' },
                { columnid: 'CurrentAddress', title: 'Correspondence Address' },
                { columnid: 'PermanentAddress', title: 'Permanent Address' },
                { columnid: 'FatherMotherContactNo', title: 'Parents Mobile No.' },
                { columnid: 'LocalToVadodara', title: 'Local To Vadodara' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'FeeCategoryName', title: 'IsFee Category Name' },
                { columnid: 'PRNGenerated', title: 'PRN Generated' },
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
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
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

});

