using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Menu:BaseEntity,IBaseEntity
    {
        public Sys_Menu()
        {
            Sys_Permissions = new List<Sys_Permission>();
        }

      
        /// <summary>
        /// �˵���
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// url��ַ
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// ҳ�����
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// ͼ��
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public Nullable<System.Guid> ParentId { get; set; }
        /// <summary>
        /// �ϼ��˵���
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// �㼶���
        /// </summary>
        public Nullable<int> Level { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Nullable<int> Sort { get; set; }
        /// <summary>
        /// �Ƿ���ã�0������1�����ã�
        /// </summary>
        public Nullable<int> Status { get; set; }
        /// <summary>
        /// �Ƿ���ɾ��
        /// </summary>
        public Nullable<int> IsDeleted { get; set; }
        /// <summary>
        /// �Ƿ�Ĭ��·��
        /// </summary>
        public Nullable<int> IsDefaultRouter { get; set; }
        /// <summary>
        /// ǰ�������.vue��
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// �ڲ˵�������
        /// </summary>
        public Nullable<int> HideInMenu { get; set; }
        /// <summary>
        /// ������ҳ��
        /// </summary>
        public Nullable<int> NotCache { get; set; }
        /// <summary>
        /// ҳ��ر�ǰ�Ļص�����
        /// </summary>
        public string BeforeCloseFun { get; set; }

        public virtual List<Sys_Permission> Sys_Permissions { get; set; }
    }
}
