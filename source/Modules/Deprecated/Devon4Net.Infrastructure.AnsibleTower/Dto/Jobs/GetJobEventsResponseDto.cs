using Devon4Net.Infrastructure.AnsibleTower.Dto.Common;

namespace Devon4Net.Infrastructure.AnsibleTower.Dto.Jobs
{
    public class GetJobEventsResponseDto
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public Related_job_events related { get; set; }
        public Summary_Fields_job_events summary_fields { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public int? job { get; set; }
        public string _event { get; set; }
        public int? counter { get; set; }
        public string event_display { get; set; }
        public Event_Data event_data { get; set; }
        public int? event_level { get; set; }
        public bool failed { get; set; }
        public bool changed { get; set; }
        public string uuid { get; set; }
        public string parent_uuid { get; set; }
        public int? host { get; set; }
        public string host_name { get; set; }
        public string playbook { get; set; }
        public string play { get; set; }
        public string task { get; set; }
        public string role { get; set; }
        public string stdout { get; set; }
        public int? start_line { get; set; }
        public int? end_line { get; set; }
        public int? verbosity { get; set; }
    }

    public class Related_job_events
    {
        public string job { get; set; }
        public string children { get; set; }
        public string host { get; set; }
    }

    public class Summary_Fields_job_events
    {
        public Job job { get; set; }
        public object role { get; set; }
        public Host host { get; set; }
    }

    public class Job
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public bool failed { get; set; }
        public float? elapsed { get; set; }
        public string type { get; set; }
        public int? job_template_id { get; set; }
        public string job_template_name { get; set; }
    }


    public class Host
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Event_Data
    {
        public string playbook { get; set; }
        public string playbook_uuid { get; set; }
        public string uuid { get; set; }
        public string play { get; set; }
        public string play_uuid { get; set; }
        public string play_pattern { get; set; }
        public string name { get; set; }
        public string pattern { get; set; }
        public string task { get; set; }
        public string task_uuid { get; set; }
        public string task_action { get; set; }
        public string task_args { get; set; }
        public string task_path { get; set; }
        public string host { get; set; }
        public bool is_conditional { get; set; }
        public string remote_addr { get; set; }
        public Res res { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public float? duration { get; set; }
        public object event_loop { get; set; }
        public object changed { get; set; }
        public object dark { get; set; }
        public object failures { get; set; }
        public object ignored { get; set; }
        public Ok ok { get; set; }
        public Processed processed { get; set; }
        public object rescued { get; set; }
        public object skipped { get; set; }
        public object artifact_data { get; set; }
    }

    public class Res
    {
        public string msg { get; set; }
        public bool _ansible_verbose_always { get; set; }
        public bool _ansible_no_log { get; set; }
        public bool changed { get; set; }
        public Ansible_Facts ansible_facts { get; set; }
        public object[] warnings { get; set; }
        public object[] deprecations { get; set; }
        public bool _ansible_verbose_override { get; set; }
    }

    public class Ansible_Facts
    {
        public string ansible_virtualization_type { get; set; }
        public string ansible_virtualization_role { get; set; }
        public string ansible_system { get; set; }
        public string ansible_kernel { get; set; }
        public string ansible_kernel_version { get; set; }
        public string ansible_machine { get; set; }
        public string ansible_python_version { get; set; }
        public string ansible_fqdn { get; set; }
        public string ansible_hostname { get; set; }
        public string ansible_nodename { get; set; }
        public string ansible_domain { get; set; }
        public string ansible_userspace_bits { get; set; }
        public string ansible_architecture { get; set; }
        public string ansible_userspace_architecture { get; set; }
        public string ansible_machine_id { get; set; }
        public string ansible_distribution { get; set; }
        public string ansible_distribution_release { get; set; }
        public string ansible_distribution_version { get; set; }
        public string ansible_distribution_major_version { get; set; }
        public string ansible_distribution_file_path { get; set; }
        public string ansible_distribution_file_variety { get; set; }
        public bool ansible_distribution_file_parsed { get; set; }
        public string ansible_os_family { get; set; }
        public string ansible_user_id { get; set; }
        public int? ansible_user_uid { get; set; }
        public int? ansible_user_gid { get; set; }
        public string ansible_user_gecos { get; set; }
        public string ansible_user_dir { get; set; }
        public string ansible_user_shell { get; set; }
        public int? ansible_real_user_id { get; set; }
        public int? ansible_effective_user_id { get; set; }
        public int? ansible_real_group_id { get; set; }
        public int? ansible_effective_group_id { get; set; }
        public bool ansible_is_chroot { get; set; }
        public Ansible_Dns ansible_dns { get; set; }
        public string ansible_system_capabilities_enforced { get; set; }
        public string[] ansible_system_capabilities { get; set; }
        public object[] ansible_fibre_channel_wwn { get; set; }
        public string[] ansible_processor { get; set; }
        public int? ansible_processor_count { get; set; }
        public int? ansible_processor_cores { get; set; }
        public int? ansible_processor_threads_per_core { get; set; }
        public int? ansible_processor_vcpus { get; set; }
        public int? ansible_memtotal_mb { get; set; }
        public int? ansible_memfree_mb { get; set; }
        public int? ansible_swaptotal_mb { get; set; }
        public int? ansible_swapfree_mb { get; set; }
        public Ansible_Memory_Mb ansible_memory_mb { get; set; }
        public string ansible_bios_date { get; set; }
        public string ansible_bios_version { get; set; }
        public string ansible_form_factor { get; set; }
        public string ansible_product_name { get; set; }
        public string ansible_product_serial { get; set; }
        public string ansible_product_uuid { get; set; }
        public string ansible_product_version { get; set; }
        public string ansible_system_vendor { get; set; }
        public Ansible_Devices ansible_devices { get; set; }
        public Ansible_Device_Links ansible_device_links { get; set; }
        public int? ansible_uptime_seconds { get; set; }
        public Ansible_Mounts[] ansible_mounts { get; set; }
        public string[] ansible_interfaces { get; set; }
        public Ansible_Eth0 ansible_eth0 { get; set; }
        public Ansible_Lo ansible_lo { get; set; }
        public Ansible_Default_Ipv4 ansible_default_ipv4 { get; set; }
        public object ansible_default_ipv6 { get; set; }
        public string[] ansible_all_ipv4_addresses { get; set; }
        public object[] ansible_all_ipv6_addresses { get; set; }
        public string ansible_hostnqn { get; set; }
        public string ansible_iscsi_iqn { get; set; }
        public string ansible_service_mgr { get; set; }
        public bool ansible_fips { get; set; }
        public Ansible_Python ansible_python { get; set; }
        public string ansible_pkg_mgr { get; set; }
        public Ansible_Apparmor ansible_apparmor { get; set; }
        public Ansible_Date_Time ansible_date_time { get; set; }
        public Ansible_Cmdline ansible_cmdline { get; set; }
        public Ansible_Proc_Cmdline ansible_proc_cmdline { get; set; }
        public object ansible_local { get; set; }
        public object ansible_lsb { get; set; }
        public bool ansible_selinux_python_present { get; set; }
        public Ansible_Selinux ansible_selinux { get; set; }
        public string[] gather_subset { get; set; }
        public bool module_setup { get; set; }
    }

    public class Ansible_Dns
    {
        public string[] search { get; set; }
        public string[] nameservers { get; set; }
        public Options options { get; set; }
    }

    public class Options
    {
        public string ndots { get; set; }
    }

    public class Ansible_Memory_Mb
    {
        public Real real { get; set; }
        public Nocache nocache { get; set; }
        public Swap swap { get; set; }
    }

    public class Real
    {
        public int? total { get; set; }
        public int? used { get; set; }
        public int? free { get; set; }
    }

    public class Nocache
    {
        public int? free { get; set; }
        public int? used { get; set; }
    }

    public class Swap
    {
        public int? total { get; set; }
        public int? free { get; set; }
        public int? used { get; set; }
        public int? cached { get; set; }
    }

    public class Ansible_Devices
    {
        public Loop1 loop1 { get; set; }
        public Loop6 loop6 { get; set; }
        public Loop4 loop4 { get; set; }
        public Sr0 sr0 { get; set; }
        public Loop2 loop2 { get; set; }
        public Loop0 loop0 { get; set; }
        public Loop7 loop7 { get; set; }
        public Sda sda { get; set; }
        public Loop5 loop5 { get; set; }
        public Loop3 loop3 { get; set; }
    }

    public class Loop1
    {
        public int? _virtual { get; set; }
        public Links links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Loop6
    {
        public int? _virtual { get; set; }
        public Links1 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links1
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }


    public class Loop4
    {
        public int? _virtual { get; set; }
        public Links2 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links2
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Sr0
    {
        public int? _virtual { get; set; }
        public Links3 links { get; set; }
        public string vendor { get; set; }
        public string model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links3
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }


    public class Loop2
    {
        public int? _virtual { get; set; }
        public Links4 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links4
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Loop0
    {
        public int? _virtual { get; set; }
        public Links5 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public Partitions5 partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links5
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Partitions5
    {
    }

    public class Loop7
    {
        public int? _virtual { get; set; }
        public Links6 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public Partitions6 partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links6
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Partitions6
    {
    }

    public class Sda
    {
        public int? _virtual { get; set; }
        public Links7 links { get; set; }
        public string vendor { get; set; }
        public string model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public Partitions7 partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links7
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Partitions7
    {
        public Sda2 sda2 { get; set; }
        public Sda1 sda1 { get; set; }
    }

    public class Sda2
    {
        public Links8 links { get; set; }
        public string start { get; set; }
        public string sectors { get; set; }
        public int? sectorsize { get; set; }
        public string size { get; set; }
        public object uuid { get; set; }
        public object[] holders { get; set; }
    }

    public class Links8
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Sda1
    {
        public Links9 links { get; set; }
        public string start { get; set; }
        public string sectors { get; set; }
        public int? sectorsize { get; set; }
        public string size { get; set; }
        public object uuid { get; set; }
        public object[] holders { get; set; }
    }

    public class Links9
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Loop5
    {
        public int? _virtual { get; set; }
        public Links10 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public Partitions8 partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links10
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }

    public class Partitions8
    {
    }

    public class Loop3
    {
        public int? _virtual { get; set; }
        public Links11 links { get; set; }
        public object vendor { get; set; }
        public object model { get; set; }
        public object sas_address { get; set; }
        public object sas_device_handle { get; set; }
        public string removable { get; set; }
        public string support_discard { get; set; }
        public object partitions { get; set; }
        public string rotational { get; set; }
        public string scheduler_mode { get; set; }
        public string sectors { get; set; }
        public string sectorsize { get; set; }
        public string size { get; set; }
        public string host { get; set; }
        public object[] holders { get; set; }
    }

    public class Links11
    {
        public object[] ids { get; set; }
        public object[] uuids { get; set; }
        public object[] labels { get; set; }
        public object[] masters { get; set; }
    }


    public class Ansible_Device_Links
    {
        public object ids { get; set; }
        public object uuids { get; set; }
        public Labels labels { get; set; }
        public object masters { get; set; }
    }


    public class Ansible_Eth0
    {
        public string device { get; set; }
        public string macaddress { get; set; }
        public int? mtu { get; set; }
        public bool active { get; set; }
        public string type { get; set; }
        public int? speed { get; set; }
        public bool promisc { get; set; }
        public Ipv4 ipv4 { get; set; }
        public Features features { get; set; }
        public string[] timestamping { get; set; }
        public object[] hw_timestamp_filters { get; set; }
    }

    public class Ipv4
    {
        public string address { get; set; }
        public string broadcast { get; set; }
        public string netmask { get; set; }
        public string network { get; set; }
    }

    public class Features
    {
        public string rx_checksumming { get; set; }
        public string tx_checksumming { get; set; }
        public string tx_checksum_ipv4 { get; set; }
        public string tx_checksum_ip_generic { get; set; }
        public string tx_checksum_ipv6 { get; set; }
        public string tx_checksum_fcoe_crc { get; set; }
        public string tx_checksum_sctp { get; set; }
        public string scatter_gather { get; set; }
        public string tx_scatter_gather { get; set; }
        public string tx_scatter_gather_fraglist { get; set; }
        public string tcp_segmentation_offload { get; set; }
        public string tx_tcp_segmentation { get; set; }
        public string tx_tcp_ecn_segmentation { get; set; }
        public string tx_tcp_mangleid_segmentation { get; set; }
        public string tx_tcp6_segmentation { get; set; }
        public string generic_segmentation_offload { get; set; }
        public string generic_receive_offload { get; set; }
        public string large_receive_offload { get; set; }
        public string rx_vlan_offload { get; set; }
        public string tx_vlan_offload { get; set; }
        public string ntuple_filters { get; set; }
        public string receive_hashing { get; set; }
        public string highdma { get; set; }
        public string rx_vlan_filter { get; set; }
        public string vlan_challenged { get; set; }
        public string tx_lockless { get; set; }
        public string netns_local { get; set; }
        public string tx_gso_robust { get; set; }
        public string tx_fcoe_segmentation { get; set; }
        public string tx_gre_segmentation { get; set; }
        public string tx_gre_csum_segmentation { get; set; }
        public string tx_ipxip4_segmentation { get; set; }
        public string tx_ipxip6_segmentation { get; set; }
        public string tx_udp_tnl_segmentation { get; set; }
        public string tx_udp_tnl_csum_segmentation { get; set; }
        public string tx_gso_partial { get; set; }
        public string tx_sctp_segmentation { get; set; }
        public string tx_esp_segmentation { get; set; }
        public string tx_udp_segmentation { get; set; }
        public string fcoe_mtu { get; set; }
        public string tx_nocache_copy { get; set; }
        public string loopback { get; set; }
        public string rx_fcs { get; set; }
        public string rx_all { get; set; }
        public string tx_vlan_stag_hw_insert { get; set; }
        public string rx_vlan_stag_hw_parse { get; set; }
        public string rx_vlan_stag_filter { get; set; }
        public string l2_fwd_offload { get; set; }
        public string hw_tc_offload { get; set; }
        public string esp_hw_offload { get; set; }
        public string esp_tx_csum_hw_offload { get; set; }
        public string rx_udp_tunnel_port_offload { get; set; }
        public string tls_hw_tx_offload { get; set; }
        public string tls_hw_rx_offload { get; set; }
        public string rx_gro_hw { get; set; }
        public string tls_hw_record { get; set; }
    }

    public class Ansible_Lo
    {
        public string device { get; set; }
        public int? mtu { get; set; }
        public bool active { get; set; }
        public string type { get; set; }
        public bool promisc { get; set; }
        public Ipv41 ipv4 { get; set; }
        public Features1 features { get; set; }
        public string[] timestamping { get; set; }
        public object[] hw_timestamp_filters { get; set; }
    }

    public class Ipv41
    {
        public string address { get; set; }
        public string broadcast { get; set; }
        public string netmask { get; set; }
        public string network { get; set; }
    }

    public class Features1
    {
        public string rx_checksumming { get; set; }
        public string tx_checksumming { get; set; }
        public string tx_checksum_ipv4 { get; set; }
        public string tx_checksum_ip_generic { get; set; }
        public string tx_checksum_ipv6 { get; set; }
        public string tx_checksum_fcoe_crc { get; set; }
        public string tx_checksum_sctp { get; set; }
        public string scatter_gather { get; set; }
        public string tx_scatter_gather { get; set; }
        public string tx_scatter_gather_fraglist { get; set; }
        public string tcp_segmentation_offload { get; set; }
        public string tx_tcp_segmentation { get; set; }
        public string tx_tcp_ecn_segmentation { get; set; }
        public string tx_tcp_mangleid_segmentation { get; set; }
        public string tx_tcp6_segmentation { get; set; }
        public string generic_segmentation_offload { get; set; }
        public string generic_receive_offload { get; set; }
        public string large_receive_offload { get; set; }
        public string rx_vlan_offload { get; set; }
        public string tx_vlan_offload { get; set; }
        public string ntuple_filters { get; set; }
        public string receive_hashing { get; set; }
        public string highdma { get; set; }
        public string rx_vlan_filter { get; set; }
        public string vlan_challenged { get; set; }
        public string tx_lockless { get; set; }
        public string netns_local { get; set; }
        public string tx_gso_robust { get; set; }
        public string tx_fcoe_segmentation { get; set; }
        public string tx_gre_segmentation { get; set; }
        public string tx_gre_csum_segmentation { get; set; }
        public string tx_ipxip4_segmentation { get; set; }
        public string tx_ipxip6_segmentation { get; set; }
        public string tx_udp_tnl_segmentation { get; set; }
        public string tx_udp_tnl_csum_segmentation { get; set; }
        public string tx_gso_partial { get; set; }
        public string tx_sctp_segmentation { get; set; }
        public string tx_esp_segmentation { get; set; }
        public string tx_udp_segmentation { get; set; }
        public string fcoe_mtu { get; set; }
        public string tx_nocache_copy { get; set; }
        public string loopback { get; set; }
        public string rx_fcs { get; set; }
        public string rx_all { get; set; }
        public string tx_vlan_stag_hw_insert { get; set; }
        public string rx_vlan_stag_hw_parse { get; set; }
        public string rx_vlan_stag_filter { get; set; }
        public string l2_fwd_offload { get; set; }
        public string hw_tc_offload { get; set; }
        public string esp_hw_offload { get; set; }
        public string esp_tx_csum_hw_offload { get; set; }
        public string rx_udp_tunnel_port_offload { get; set; }
        public string tls_hw_tx_offload { get; set; }
        public string tls_hw_rx_offload { get; set; }
        public string rx_gro_hw { get; set; }
        public string tls_hw_record { get; set; }
    }

    public class Ansible_Default_Ipv4
    {
        public string gateway { get; set; }
        public string _interface { get; set; }
        public string address { get; set; }
        public string broadcast { get; set; }
        public string netmask { get; set; }
        public string network { get; set; }
        public string macaddress { get; set; }
        public int? mtu { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
    }

    public class Ansible_Python
    {
        public Version version { get; set; }
        public object[] version_info { get; set; }
        public string executable { get; set; }
        public bool has_sslcontext { get; set; }
        public string type { get; set; }
    }

    public class Version
    {
        public int? major { get; set; }
        public int? minor { get; set; }
        public int? micro { get; set; }
        public string releaselevel { get; set; }
        public int? serial { get; set; }
    }

    public class Ansible_Apparmor
    {
        public string status { get; set; }
    }

    public class Ansible_Date_Time
    {
        public string year { get; set; }
        public string month { get; set; }
        public string weekday { get; set; }
        public string weekday_number { get; set; }
        public string weeknumber { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
        public string epoch { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public DateTime iso8601_micro { get; set; }
        public DateTime iso8601 { get; set; }
        public string iso8601_basic { get; set; }
        public string iso8601_basic_short { get; set; }
        public string tz { get; set; }
        public string tz_offset { get; set; }
    }

    public class Ansible_Cmdline
    {
        public string BOOT_IMAGE { get; set; }
        public string root { get; set; }
        public bool ro { get; set; }
    }

    public class Ansible_Proc_Cmdline
    {
        public string BOOT_IMAGE { get; set; }
        public string root { get; set; }
        public bool ro { get; set; }
    }


    public class Ansible_Selinux
    {
        public string status { get; set; }
    }

    public class Ansible_Mounts
    {
        public string mount { get; set; }
        public string device { get; set; }
        public string fstype { get; set; }
        public string options { get; set; }
        public long size_total { get; set; }
        public long size_available { get; set; }
        public int? block_size { get; set; }
        public int? block_total { get; set; }
        public int? block_available { get; set; }
        public int? block_used { get; set; }
        public int? inode_total { get; set; }
        public int? inode_available { get; set; }
        public int? inode_used { get; set; }
        public string uuid { get; set; }
    }


    public class Ok
    {
        public int? localhost { get; set; }
    }

    public class Processed
    {
        public int? localhost { get; set; }
    }

}
