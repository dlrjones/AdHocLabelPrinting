Version 3

This app uses the PrintDocument class and overrides the PrintPage method. Everything has to happen within this method which is why you see so many filters.

// DB Password
/*
The enrypted password for the DB access is stored in the file [app root dir]\rsrc.txt.
The referenced library KeyMaster is used to decrypt the password at run time. There is another app called EncryptAndHash which provides a front
end to KeyMaster that you can use to change the password when that becomes necessary. The key is sv_pmm_jobs and the path to EncryptAndHash is 
\\Lapis\h_purchasing$\Purchasing\PMM IS data\HEMM Apps\Executables\EncryptAndHash.exe.
*/


Ray is running this from 
\\lapis\rjb$\AdHocLabelPrinting

I have access to this directory to be able to provide access